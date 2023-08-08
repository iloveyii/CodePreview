<?php

namespace Drupal\actioner\EventSubscriber;


use Drupal\Component\EventDispatcher\Event;
use Symfony\Component\EventDispatcher\EventSubscriberInterface;
use Drupal\views_bulk_operations\ViewsBulkOperationsEvent;


/**
 * Event that is fired when a user logs in.
 */
class VboEventSubscriber implements EventSubscriberInterface
{
  /**
   * {@inheritdoc}
   */
  public static function getSubscribedEvents()
  {
    $events = [];
    // The next line prevents hard dependency on VBO module.
    if (class_exists(ViewsBulkOperationsEvent::class)) {
      $events['views_bulk_operations.view_data'][] = ['provideViewData', 0];
    }
    // \Drupal::logger('actioner')->notice("Actioner getSubscribedEvents ");
    return $events;
  }

  /**
   * Provide entity type data and entity getter to VBO.
   *
   * @param \Drupal\views_bulk_operations\ViewsBulkOperationsEvent $event
   *   The event object.
   */
  public function provideViewData($event)
  {
    \Drupal::logger('actioner')->notice("Actioner provideViewData ");
    // if ($event->getProvider() === 'some_module') {
    //   $event->setEntityTypeIds(['node']);
    //   $event->setEntityGetter([
    //     'file' => drupal_get_path('module', 'some_module') . '/src/someClass.php',
    //     'callable' => '\Drupal\some_module\someClass::getEntityFromRow',
    //   ]);
    // }
  }
}
