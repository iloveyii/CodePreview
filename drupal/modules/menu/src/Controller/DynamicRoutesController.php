<?php

/**
 * @file
 * @Contains Drupal\menu\Controller\MenuController
 */

namespace Drupal\menu\Controller;

use Drupal\Core\Controller\ControllerBase;
use Drupal\simplify_menu\MenuItems;
use Symfony\Component\DependencyInjection\ContainerInterface;



class DynamicRoutesController extends ControllerBase
{
  public function index()
  {
    $build['results'] = [
      '#markup' => '<h2> Dynamic controller</h2>'
    ];

    return $build;
  }
}
