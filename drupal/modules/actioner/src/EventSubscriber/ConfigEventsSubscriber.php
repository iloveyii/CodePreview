<?php

namespace Drupal\actioner\EventSubscriber;

use Symfony\Component\EventDispatcher\EventSubscriberInterface;
use Drupal\Core\Config\ConfigCrudEvent;
use Drupal\Core\Config\ConfigEvents;


class ConfigEventsSubscriber implements EventSubscriberInterface
{

    public static function getSubscribedEvents()
    {
        return [
            ConfigEvents::SAVE => 'configSave',
            ConfigEvents::DELETE => 'configDelete',
        ];
    }

    /**
     * React to a config object being saved.
     *
     * @param \Drupal\Core\Config\ConfigCrudEvent $event
     *   Config crud event.
     */
    public function configSave(ConfigCrudEvent $event)
    {
        $config = $event->getConfig();
        \Drupal::messenger()->addStatus('Saved config: ' . $config->getName());
    }

    /**
     * React to a config object being deleted.
     *
     * @param \Drupal\Core\Config\ConfigCrudEvent $event
     *   Config crud event.
     */
    public function configDelete(ConfigCrudEvent $event)
    {
        $config = $event->getConfig();
        \Drupal::messenger()->addStatus('Deleted config: ' . $config->getName());
    }
}
