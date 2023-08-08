<?php

/**
 * @file
 * Contains Drupal\repair\Utils\TermHelper
 */

namespace Drupal\repair\Utils;

use Drupal\field\Entity\FieldConfig;
use Drupal\field\Entity\FieldStorageConfig;
use Drupal\file\Entity\File;
use Drupal\image\Entity\ImageStyle;
use Drupal\media\Entity\Media;
use Drupal\media\Entity\MediaType;
use Drupal\node\Entity\Node;
use Drupal\paragraphs\Entity\Paragraph;
use \Drupal\taxonomy\Entity\Term;

class TermHelper
{
  /**
   * Get all the terms of a vid.
   *
   * @param string $mid
   * @param string $imageStyle
   * @return mixed
   */
  public static function loadMultiple($vid, $id_only = false)
  {
    $terms = Term::loadMultiple();
  }

  /**
   * Add to to vid
   * @param integer $vid
   * @param string $name
   */
  public static function create_term($vid, $name, $parent = [], $target_id = 191)
  {
    $term = Term::create([
      'parent' => $parent,
      'name' => $name,
      'vid' => $vid,
      'field_image' => [
        'target_id' => $target_id,
        'alt' => t($name),
        'title' => t($name),
      ],
    ]);
    $term->save();
    return $term;
  }

  /**
   * Get device id by name
   */
  public static function device_id_by_name($name)
  {
    $devices = Tax::getTermChildrenByParentId('devices', 0);
    foreach ($devices as $device) {
      if (strtolower($device['name']) == strtolower($name)) {
        return $device['tid'];
      }
    }
    return null;
  }

  /**
   * Get vendor id by name
   */
  public static function vendor_id_by_name($device_name, $name)
  {
    $device_id = static::device_id_by_name($device_name);
    $vendors = Tax::getTermChildrenByParentId('devices', $device_id);
    foreach ($vendors as $vendor) {
      if (strtolower($vendor['name']) ==  strtolower($name)) {
        return $vendor['tid'];
      }
    }
    return null;
  }

  /**
   * Get Other/Övrigt by vendor_id
   */
  public static function other_id_by_vendor_id($vendor_id)
  {
    $name = 'Övrigt';
    $vendors = Tax::getTermChildrenByParentId('devices', $vendor_id);
    foreach ($vendors as $vendor) {
      if (strtolower($vendor['name']) ==  strtolower($name)) {
        return $vendor['tid'];
      }
    }
    return null;
  }
}
