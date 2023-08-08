<?php

namespace Drupal\actioner\Util;

use Drupal\views\Views;


class Helper
{
  public static function fileExists($file_path) // hours
  {
    if (file_exists($file_path)) {
      return true;
    }
    Helper::logger("File not exists: " . $file_path);
    return false;
  }

  public static function logger($msg)
  {
    // echo $msg . PHP_EOL;
    \Drupal::logger('actioner')->notice($msg);
  }

  public static function node_type_get_names()
  {
    return array_map(function ($bundle_info) {
      return $bundle_info['label'];
    }, \Drupal::service('entity_type.bundle.info')
      ->getBundleInfo('node'));
  }

  /**
   * Get file name without extension
   * @param string $file_path
   *  Full path
   */
  public static function getFileName($file_path)
  {
    $info = pathinfo($file_path);
    return $info['filename'];
  }

  /**
   * Get View from path
   */
  public static function getViewFromPath($path = '/admin/content/uid-code', $render = false)
  {
    // Get the View title and URL from a path.
    $url = \Drupal::service('path.validator')->getUrlIfValid($path);
    // Check URL is invalid and has a route.
    if ($url && $url->isRouted()) {
      $parts = explode('.', $url->getRouteName());

      // Is it a view?
      if ($parts[0] === 'view') {
        // Get ViewExectuable.
        if ($view = Views::getView($parts[1])) {
          $view->setDisplay($parts[2]);
          $title = $view->getTitle();
          $url = $url->toString();

          // if ($render) {
          //   $result = \Drupal::service('renderer')->render($view->render());
          //   return $result;
          // }
          $view->render();
          return $view;
        }
      }
    }

    return false;
  }
}
