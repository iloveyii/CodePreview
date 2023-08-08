<?php

namespace Drupal\menu\Routing;

use Symfony\Component\Routing\Route;
use Symfony\Component\Routing\RouteCollection;
use Drupal\core\Routing\RouteSubscriberBase;


/**
 * Defines dynamic routes.
 */
class ChallengeRoutes
{
  /**
   * Provides dynamic routes.
   */
  public function routes()
  {
    // $route_collection = new RouteCollection();

    $route['menu.dynamic'] = new Route(
      'exampler',
      [
        '_controller' => '\Drupal\menu\Controller\DynamicRoutesController::index',
        '_title' => 'Challenge 00001'
      ],
      [
        '_permission' => 'access content'
      ]
    );

    return $route;
  }
}
