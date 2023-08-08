<?php

/**
 * @file
 * @Contains Drupal\application\Routing\RouteSubscriber
 */

namespace Drupal\application\Routing;

use Drupal\Core\Routing\RouteSubscriberBase;
use Symfony\Component\Routing\RouteCollection;
use Symfony\Component\Routing\Route;

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
    $all = $collection->all();
    // Change path '/user/login' to '/login'.
    if ($route = $collection->get('user.login')) {
      $route->setPath('/login');
    }
    // Always deny access to '/user/logout'.
    // Note that the second parameter of setRequirement() is a string.
    if ($route = $collection->get('user.logout')) {
      $route->setRequirement('_access', 'FALSE');
      $route->addDefaults([
        '_controller' => '\Drupal\application\Controller\ChallengeController::view',
      ]);
    }

    if ($route = $collection->get('entity.node.canonical')) {
      if ($route = $collection->get('node.view')) {
        $route->setRequirement('_access', 'FALSE');
        $route->addDefaults([
          '_controller' => '\Drupal\application\Controller\ChallengeController::view',
          '_title_callback' => '\Drupal\node\Controller\NodeViewController::title',
        ]);
        $route->setRequirement('node', '\d+');
        $route->setRequirement('_entity_access', 'node.view');
      }

      if ($route = $collection->get('entity.node.canonical')) {

        $route->setRequirements([]);
        $route->setRequirement('_access', 'access content');
        $route
          ->addDefaults([
            '_controller' => '\Drupal\application\Controller\ChallengeController::view',
            '_title_callback' => '\Drupal\node\Controller\NodeViewController::title',
          ]);
        $route->setRequirement('node', '\d+');
        $route->setRequirement('title', '^[\w\-åäöÅÄÖ]+$');
        $route->setRequirement('_entity_access', 'node.view');
        $route->setRequirement('_access', 'access content');
      }
      $collection->add('entity.node.canonical', $route);
    }
  }
}
