<?php

/**
 * @file
 * Contains Drupal\repair\Utils\Helper
 */

namespace Drupal\repair\Utils;

use Drupal\Core\File\FileSystemInterface;
use Drupal\file\Entity\File;

class FileHelper
{
  /**
   * String to filename
   *
   * Replace non-alphanumeric by _ & double spaces by single & single space by -  lowercase
   *
   * @return string
   */
  public static function filify($str, $use_underscore = false)
  {
    $str = preg_replace('/[^\da-z_.]/i', '-', strtolower($str));
    $replace = '-';
    if ($use_underscore) {
      $replace = '_';
    }
    $str = str_replace('--', $replace, $str);
    $str = str_replace('-', $replace, $str);
    return $str;
  }

  /**
   * Set file naming convention
   */
  public static function set_naming_convention($app_id, $challenge_id, $hash, $file_name)
  {
    return $new_file_name = sprintf("%d_challenge_%d_%s_%s", $app_id, $challenge_id, $hash, $file_name);
  }

  /**
   * Get filename from fid
   */
  public static function get_filename($fid)
  {
    $file = File::load($fid);
    $file_name = null;
    if ($file instanceof File) {
      $file_name = basename($file->getFileUri());
    }
    return $file_name;
  }

  /**
   * Rename a managed loaded file with fid to a new name
   *
   * @param int $fid
   * @param string $new_name
   */
  public static function rename_file($fid, $new_file_name)
  {
    $file = File::load($fid);
    if ($file instanceof File) {
      $fileUri = $file->getFileUri();
      $sourceUri = \Drupal::service('file_system')->realpath($file->getFileUri());
      $file_name = basename($sourceUri);

      $destinationUri = str_replace($file_name, '', $sourceUri) . $new_file_name;
      $newFileUri = str_replace($file_name, '', $fileUri) . $new_file_name;
      // Prepare directory
      \Drupal::service('file_system')->prepareDirectory(dirname($new_file_name));
      // Move and overwrite
      $newFileName = \Drupal::service('file_system')->move($sourceUri, $destinationUri, FileSystemInterface::EXISTS_REPLACE);

      // Set the new file path on the file entity.
      $file->setFileUri($newFileUri);
      $file->setFilename($new_file_name);

      // Set the file to permanent if needed.
      $file->setPermanent();
      // Save entity with changes.
      $file->save();
    }
  }

  /**
   * Get fid from form_state with a field
   */
  public static function get_fid($form_state, $field)
  {
    $id = null;
    $image = $form_state->getValue($field);
    if (is_array($image) && count($image) > 0) {
      $id = array_shift($image);
    }
    return $id;
  }
}
