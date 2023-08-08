<?php

namespace Drupal\actioner\Plugin\Action;

use Drupal\Component\Datetime\TimeInterface;
use Drupal\Core\Action\ActionBase;
use Drupal\Core\Config\ConfigFactoryInterface;
use Drupal\Core\Datetime\DateFormatter;
use Drupal\Core\Entity\EntityTypeManagerInterface;
use Drupal\Core\Logger\LoggerChannelFactoryInterface;
use Drupal\Core\Plugin\ContainerFactoryPluginInterface;
use Drupal\Core\Session\AccountInterface;
use Drupal\Core\Entity\EntityStorageException;
use Drupal\views_bulk_operations\Action\ViewsBulkOperationsActionBase;
use Symfony\Component\DependencyInjection\ContainerInterface;

/**
 * Send emails 
 * This works in Views bulk operations field
 * 
 * @Action(
 *  id = "send_emails_to_entities",
 *  label = @Translation("Send emails to all selected entities"),
 *  type = "node",
 *  confirm = TRUE
 * )
 */
final class SendEmailAction extends ViewsBulkOperationsActionBase implements ContainerFactoryPluginInterface
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

    /**
     * {@inheritdoc}
     */
    public function execute($entity = NULL)
    {
        if ($entity->getSticky() == 1) {
            $this->messenger()->addError($this->t('You have already sent and email for this entity.'));
            return;
        }

        //Grab the uid from the current logged in user.
        $current_user_id = $this->account->id();
        //Grab the entities data.
        $data = $entity->getData();
        //Format the dates.
        $new_start_date = $this->dateFormatter->format(strtotime($data['booking_start_date']), 'custom', $this->date_format);
        $new_end_date = $this->dateFormatter->format(strtotime($data['booking_end_date']), 'custom', $this->date_format);

        //Setup an array and stored the values into the array.
        $values = [
            'id' => NULL,
            'type' => $this->entity_type,
            'name' => $data['booking_group_name'],
            'field_booking_date' => [
                'value' => $new_start_date,
                'end_value' => $new_end_date
            ],
            'field_booking_address' => [
                'country_code' => 'GB',
                'address_line1' => $data['booking_your_address']['address'],
                'address_line2' => empty($data['booking_your_address']['address2']) ?: $data['booking_your_address']['address2'],
                'locality' => $data['booking_your_address']['city'],
                'postal_code' => $data['booking_your_address']['postal_code'],
            ],
            'field_booking_anticipated_nos' => $data['booking_anticipated_numbers'],
            'field_booking_email' => $data['booking_your_email'],
            'field_booking_group_name' => $data['booking_group_name'],
            'field_booking_home_phone_number' => $data['booking_home_phone_number'],
            'field_booking_mobile_number' => $data['booking_your_mobile_number'],
            'field_booking_name' => $data['booking_your_name'],
            'field_booking_special_reqs' => $data['booking_special_requirements'],
            'field_booking_centre' => $data['booking_centre'],
            'field_booking_camping' => $data['booking_camping'],
            'field_number_of_tents' => $data['no_of_tents'],
            'field_booking_status' => 29,
            'uid' => $current_user_id,
            'revision_user' => $current_user_id,
            'status' => 1,
            'created' => $this->timeInterface->getRequestTime(),
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
    }

    /**
     * {@inheritdoc}
     */
    public function access($object, ?AccountInterface $account = NULL, $return_as_object = FALSE)
    {
        $result = $object->access('update', $account, TRUE);
        return $return_as_object ? $result : $result->isAllowed();
    }
}
