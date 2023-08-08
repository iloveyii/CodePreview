<?php

/**
 * @file
 * @Contains Drupal\repair\Model\Model
 */

namespace Drupal\repair\Model;

use Drupal\repair\Utils\Constant;
use Drupal\repair\Utils\Helper;
use Drupal\repair\Utils\Tax;
use Drupal\taxonomy\Entity\Term;

/**
 * Model list
 *
 * This provides the repair page
 */
class Model
{
  /**
   * Load all nodes of Vendor
   */
  public static function loadMultiple($vendor_id = 594)
  {
    $models = Tax::getTermChildrenByParentId('devices', $vendor_id, 'thumbnail');
    return $models;
  }

  /**
   * Find by name
   */
  public static function findByName($name)
  {
    $models = static::loadMultiple();
  }

  /**
   * Autocomplete
   */
  public static function autocomplete($name, $vendor_id = null)
  {
    $matches[] = [
      'value' => '',
      'label' => 'No results found'
    ];
    if (strlen($name) > 2) {
      $terms = static::loadMultiple($vendor_id);
      if (is_array($terms)) {
        $matches = [];
        foreach ($terms as $term) {
          $p = 0;
          similar_text($name, $term['name'], $p);
          if ($p > 10) {
            $matches[] = [
              'value' => $term['name'],
              'label' => $term['name'],
              'sim' => $p,
              'tid' => $term['tid'],
              'name' => $term['name'],
              'image_url' => $term['image_url'],
            ];
          }
        }
      }
      usort($matches, function ($item1, $item2) {
        return $item2['sim'] <=> $item1['sim'];
      });
      return array_slice($matches, 0, 5);
    } else {
      $matches = [];
      $terms = static::loadMultiple($vendor_id);
      foreach ($terms as $term) {
        $matches[] = [
          'value' => $term['name'],
          'label' => $term['name'],
          'sim' => $p,
          'tid' => $term['tid'],
          'name' => $term['name'],
          'image_url' => $term['image_url'],
        ];
      }
      return $matches;
    }
  }

  /**
   * Get name of model
   */
  public static function name($term)
  {
    $name = null;
    if ($term instanceof Term) {
      $name = $term->name->value;
    }
    if (is_array($term) && count($term) > 0) {
      $name = static::name($term[0]);
    }
    return $name;
  }
}
