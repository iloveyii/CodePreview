<?php

namespace Drupal\actioner\Plugin\Action;

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
 * Change Booking Status
 * This works for any node operational action
 * 
 * @Action(
 *   id = "change_booking_status",
 *   label = @Translation("Change booking status"),
 *   type = "node"
 * )
 */
final class ChangeBookingStatus extends ConfigurableActionBase implements ContainerFactoryPluginInterface
{

    /**
     * @var string
     */
    protected $entity_type = "node";

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
        $this->loggerFactory = $logger_factory->get('ratlingate_custom_actions');
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

    /**
     * {@inheritdoc}
     */
    public function execute($entity = NULL)
    {
        $entity->field_booking_status->target_id = $this->configuration['booking_status'];
        try {
            $entity->save();
            $this->messenger()->addStatus($this->t('Booking status has been updated.'));
        } catch (EntityStorageException $e) {
            $this->messenger()->addError($this->t('There was a problem updating the booking status, please try again later.'));
            $this->loggerFactory->error('%errormsg %errorstack', ['%errormsg' => $e->getMessage(), '%errorstack' => $e->getTraceAsString()]);
        }
    }

    /**
     * {@inheritdoc}
     */
    protected function getDefaultConfiguration()
    {
        return [
            'booking_status' => '',
        ];
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
        $vid = 'booking_statuses';
        try {
            $terms = $this->entityTypeManager->getStorage('taxonomy_term')->loadTree($vid);
        } catch (InvalidPluginDefinitionException $e) {
        } catch (PluginNotFoundException $e) {
            $this->loggerFactory->error('%errormsg %errorstack', ['%errormsg' => $e->getMessage(), '%errorstack' => $e->getTraceAsString()]);
        }
        $options = [];

        foreach ($terms as $term) {
            try {
                $term_obj = $this->entityTypeManager->getStorage('taxonomy_term')->load($term->tid);
            } catch (InvalidPluginDefinitionException $e) {
            } catch (PluginNotFoundException $e) {
                $this->loggerFactory->error('%errormsg %errorstack', ['%errormsg' => $e->getMessage(), '%errorstack' => $e->getTraceAsString()]);
            }
            $options[$term_obj->id()] = $term_obj->label();
        }

        $form['booking_status'] = [
            '#type' => 'select',
            '#title' => t('Booking Status'),
            '#description' => t('The booking status that the booking should be changed to'),
            '#default_value' => $this->configuration['booking_status'],
            '#options' => $options,
            '#required' => TRUE,
        ];
        return $form;
    }

    /**
     * @inheritDoc
     */
    public function submitConfigurationForm(array &$form, FormStateInterface $form_state)
    {
        $this->configuration['booking_status'] = $form_state->getValue('booking_status');
    }
}
