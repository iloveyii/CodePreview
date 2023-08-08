<?php

namespace Drupal\profile\Services;

use Drupal\Core\Routing\RouteSubscriberBase;
use Symfony\Component\Routing\RouteCollection;

/**
 * Listens to the dynamic route events.
 */
class RouteSubscriber extends RouteSubscriberBase
{

    /**
     * {@inheritdoc}
     */
    protected function alterRoutes(RouteCollection $collection)
    {

        // Edit user
        if ($route = $collection->get('entity.user.edit_form')) {
            $route->setRequirement('_permission', 'administer users');
        }

        // View user
        if ($route = $collection->get('entity.user.canonical')) {
            $route->setRequirement('_permission', 'administer users');
        }
    }
}
