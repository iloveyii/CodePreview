<?php


namespace Drupal\actioner\Plugin\Action;

use Drupal\actioner\Util\Helper;
use Drupal\actioner\Util\View;
use Drupal\views_bulk_operations\Action\ViewsBulkOperationsActionBase;
use Drupal\Core\StringTranslation\StringTranslationTrait;
use Drupal\views_bulk_operations\Action\ViewsBulkOperationsPreconfigurationInterface;
use Drupal\Core\Action\ActionBase;
use Drupal\Component\Datetime\TimeInterface;
use Drupal\Component\Plugin\Exception\InvalidPluginDefinitionException;
use Drupal\Component\Plugin\Exception\PluginNotFoundException;
use Drupal\Core\Action\ConfigurableActionBase;
use Drupal\Core\Config\ConfigFactoryInterface;
use Drupal\Core\Datetime\DateFormatter;
use Drupal\Core\Entity\EntityStorageException;
use Drupal\Core\Entity\EntityTypeManagerInterface;
use Drupal\Core\Form\FormStateInterface;
use Drupal\Core\Logger\LoggerChannelFactoryInterface;
use Drupal\Core\Plugin\ContainerFactoryPluginInterface;
use Drupal\Core\Session\AccountInterface;
use Symfony\Component\DependencyInjection\ContainerInterface;


/**
 * This will export all the selected nodes on content page to CSV
 * This works for any node operational action
 *
 * @Action(
 *   id = "export_to_excel_action",
 *   label = @Translation("Export as CSV"),
 *   type = "node",
 * )
 */
class ExcelAction extends ConfigurableActionBase implements ContainerFactoryPluginInterface
{
  /**
   * @var string
   */
  protected $entity_type = "node";

  /**
   * @var string
   */
  protected $date_format = 'Y-m-d\TH:i:s';

  /**
   * @var string
   */
  protected $mail_date_format = 'd/m/Y H:i:s';

  /**
   * @var \Drupal\Core\Config\ConfigFactoryInterface
   */
  protected $configFactory;

  /**
   * @var \Drupal\Core\Session\AccountInterface
   */
  protected $account;

  /**
   * @var \Drupal\Core\Datetime\DateFormatter
   */
  protected $dateFormatter;

  /**
   * @var \Drupal\Core\Logger\LoggerChannelInterface
   */
  protected $loggerFactory;

  /**
   * @var \Drupal\Core\Entity\EntityTypeManagerInterface
   */
  protected $entityTypeManager;

  /**
   * @var \Drupal\Component\Datetime\TimeInterface
   */
  protected $timeInterface;



  /**
   * ConvertEnquiryToBooking constructor.
   * @param array $configuration
   * @param $plugin_id
   * @param $plugin_definition
   * @param ConfigFactoryInterface $config_factory
   * @param AccountInterface $account
   * @param DateFormatter $date_formatter
   * @param LoggerChannelFactoryInterface $logger_factory
   * @param EntityTypeManagerInterface $entity_type_manager
   * @param TimeInterface $time_interface
   */
  public function __construct(
    array $configuration,
    $plugin_id,
    $plugin_definition,
    ConfigFactoryInterface $config_factory,
    AccountInterface $account,
    DateFormatter $date_formatter,
    LoggerChannelFactoryInterface $logger_factory,
    EntityTypeManagerInterface $entity_type_manager,
    TimeInterface $time_interface
  ) {
    parent::__construct($configuration, $plugin_id, $plugin_definition);
    $this->configFactory = $config_factory;
    $this->account = $account;
    $this->dateFormatter = $date_formatter;
    $this->loggerFactory = $logger_factory->get('custom_actions');
    $this->entityTypeManager = $entity_type_manager;
    $this->timeInterface = $time_interface;
  }

  /**
   * @param \Symfony\Component\DependencyInjection\ContainerInterface $container
   * @param array $configuration
   * @param $plugin_id
   * @param $plugin_definition
   *
   * @return static
   */
  public static function create(ContainerInterface $container, array $configuration, $plugin_id, $plugin_definition)
  {
    return new static(
      $configuration,
      $plugin_id,
      $plugin_definition,
      $container->get('config.factory'),
      $container->get('current_user'),
      $container->get('date.formatter'),
      $container->get('logger.factory'),
      $container->get('entity_type.manager'),
      $container->get('datetime.time')
    );
  }



  protected function getData()
  {
    $view = Helper::getViewFromPath('/admin/content/uid-code');
    $rows = View::getFields($view);
  }


  /**
   * {@inheritdoc}
   */
  public function execute($entity = null)
  {
    // Do some processing..
    // if ($entity->getSticky() == 1) {
    //   $this->messenger()->addError($this->t('You have already created a booking for this enquiry.'));
    //   return;
    // }

    \Drupal::logger('actioner')->notice('Actioner, entity id you selected is ' . $entity->id() . print_r($_REQUEST, true));


    return;

    //Grab the uid from the current logged in user.
    $current_user_id = $this->account->id();
    //Grab the entities data.
    $data = $entity->getData();
    //Format the dates.
    $new_start_date = $this->dateFormatter->format(strtotime($data['created']), 'custom', $this->date_format);
    $new_end_date = $this->dateFormatter->format(strtotime($data['created']), 'custom', $this->date_format);

    //Setup an array and stored the values into the array.
    $values = [
      'id' => NULL,
      'type' => $this->entity_type,
      'name' => $data['title'],
    ];

    if ($entity->getSticky() == 0) {

      try {
        //If no booking exists, create it.
        $booking_storage = $this->entityTypeManager->getStorage($this->entity_type);
        $booking = $booking_storage->create($values);
        $booking->save();
        $booking_msg = "Booking created: " . $data['booking_name'];
        //Log an issue if something went wrong.
        $this->loggerFactory->notice($this->t($booking_msg));
        $entity->setSticky(TRUE)->save();
        $webform_msg = "Booking enquiry marked as flagged, which means that the booking has been created.";
        $this->loggerFactory->notice($this->t($webform_msg));
      } catch (EntityStorageException $e) {
        $message = "Could not save booking.";
        //Log an issue if something went wrong.
        $this->loggerFactory->error($this->t($message));
      }
    }

    // Don't return anything for a default completion message, otherwise return translatable markup.
    return $this->t('Some result');
  }

  /**
   * {@inheritdoc}
   */
  public function access2($object, AccountInterface $account = NULL, $return_as_object = FALSE)
  {
    // If certain fields are updated, access should be checked against them as well.
    // @see Drupal\Core\Field\FieldUpdateActionBase::access().
    return $object->access('update', $account, $return_as_object);

    $result = $object->access('update', $account, TRUE);
    return $return_as_object ? $result : $result->isAllowed();
  }

  /**
   * {@inheritdoc}
   */
  public function access($object, AccountInterface $account = NULL, $return_as_object = FALSE)
  {
    $result = $object->access('update', $account, TRUE);
    return $return_as_object ? $result : $result->isAllowed();
  }

  /**
   * @inheritDoc
   */
  public function buildConfigurationForm(array $form, FormStateInterface $form_state)
  {
    $options = Helper::node_type_get_names();

    $form['bundles'] = [
      '#type' => 'select',
      '#title' => t('Select bundles to export'),
      '#description' => t('Only the bundles of the selected types can be exported'),
      '#default_value' => $this->configuration['bundles'],
      '#options' => $options,
      '#required' => TRUE,
    ];

    $form['selected_all'] = [
      '#type' => 'select',
      '#title' => t('Export All or Selected Nodes ?'),
      '#description' => t('All or the selected nodes will be exported'),
      '#default_value' => $this->configuration['selected_all'],
      '#options' => ['Selected', 'All'],
      '#required' => TRUE,
    ];

    return $form;
  }

  /**
   * @inheritDoc
   */
  public function submitConfigurationForm(array &$form, FormStateInterface $form_state)
  {
    $this->configuration['max'] = $form_state->getValue('max');
  }

  /**
   * {@inheritdoc}
   */
  protected function getDefaultConfiguration()
  {
    return [
      'bundles' => ['article', 'general_page'],
      'selected_all' => ['Selected', 'All'],
    ];
  }
}
