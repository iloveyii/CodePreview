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
 * This will approve/reject application forms
 *
 * This works for any node operational action
 *
 * @Action(
 *   id = "approve_application_form_action",
 *   label = @Translation("Approve selected"),
 *   type = "node",
 * )
 */
class ApproveAction extends ConfigurableActionBase implements ContainerFactoryPluginInterface
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
    if ($entity->bundle() ==  $this->configuration['bundles']) {
      \Drupal::logger('actioner')->notice('Actioner, bundle are same ');
    } else {
      \Drupal::logger('actioner')->notice('Actioner, bundle are same for id ' . $entity->id());
      return false;
    }

    if (1 ==  $this->configuration['approve_reject']) {
      \Drupal::logger('actioner')->notice('Actioner, approve it ');
      if ($entity->hasField('field_approved')) {
        $entity->set('field_approved', 1);
      }
    }

    if (0 ==  $this->configuration['approve_reject']) {
      \Drupal::logger('actioner')->notice('Actioner, reject it ');
      if ($entity->hasField('field_approved')) {
        $entity->set('field_approved', 0);
      }
    }

    $entity->save();
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
      '#title' => t('Select bundle for this action'),
      '#description' => t('Action applies to the selected bundle type'),
      '#default_value' => $this->configuration['bundles'],
      '#options' => $options,
      '#required' => TRUE,
    ];

    $form['approve_reject'] = [
      '#type' => 'radios',
      '#title' => t('Approve or Reject'),
      '#default_value' =>  $this->configuration['approve_reject'],
      '#options' => [
        '0' => t('Reject'),
        '1' => t('Approve'),
      ],
    ];

    return $form;
  }

  /**
   * @inheritDoc
   */
  public function submitConfigurationForm(array &$form, FormStateInterface $form_state)
  {
    $this->configuration['bundles'] = $form_state->getValue('bundles');
    $this->configuration['selected_all'] = $form_state->getValue('selected_all');
    $this->configuration['approve_reject2'] = $form_state->getValue('approve_reject2');
    $this->configuration['approve_reject'] = $form_state->getValue('approve_reject');
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
