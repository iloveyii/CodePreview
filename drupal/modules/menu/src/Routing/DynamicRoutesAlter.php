<?php

namespace Drupal\menu\Routing;

use Symfony\Component\Routing\Route;
use Symfony\Component\Routing\RouteCollection;
use Drupal\core\Routing\RouteSubscriberBase;


/**
 * Defines dynamic routes.
 */
class DynamicRoutesAlter extends RouteSubscriberBase
{
  /**
   * {@inheritdoc}
   */
  public function alterRoutes(RouteCollection $collection)
  {
    $route = new Route(
      'dynamic',
      [
        '_controller' => '\Drupal\menu\Controller\DynamicRoutesController::index',
        '_title' => 'Index 00001'
      ],
      [
        '_permission' => 'access content'
      ]
    );
    $collection->add('menu.dynamic_routes', $route);
  }
}
