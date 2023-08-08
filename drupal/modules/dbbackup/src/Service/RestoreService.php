<?php

/**
 * @file
 * Contains Drupal\dbbackup\Service\RestoreService
 */

namespace Drupal\dbbackup\Service;

use Drupal\dbbackup\Models\Helper;
use Drupal\dbbackup\Models\DumpRestore;
use Drupal\dbbackup\Models\SerializedRestore;
use Drupal\dbbackup\Models\Restore;


class RestoreService
{
  /**
   * Dump restore
   *
   * Restore the database from dump file
   */
  public function restoreDump()
  {
    $context = new Restore(new DumpRestore('cache_db', 'cms', 'cms_user', 'cms_user'));
    if ($context->isEnabled()) {
      if ($context->startRestore()) {
        return $context->getStrategy();
      }
      sleep(3);
      Helper::logger("The DumpRestore service is enabled but the restore failed");
      return false;
    }
    Helper::logger("The DumpRestore service is not enabled");
    return false;
  }

  /**
   * Check if the service is enabled
   */
  public function isEnabledRestoreDump()
  {
    $context = new Restore(new DumpRestore('cache_db', 'cms', 'cms_user', 'cms_user'));
    return $context->isEnabled();
  }


  /**
   * Serialized restore
   *
   * Restore the database from dump file
   */
  public function restoreSerialized($selected_types)
  {
    $context = new Restore(new SerializedRestore($selected_types));
    if ($context->isEnabled()) {
      if ($context->startRestore()) {
        Helper::logger("The SerializedRestore process succeeded!");
        return $context->getStrategy();
      }
      sleep(3);
      Helper::logger("The SerializedRestore service is enabled but the restore failed");
      return false;
    }
    Helper::logger("The SerializedRestore service is not enabled");
    return false;
  }

  /**
   * Check if the service is enabled
   */
  public function isEnabledRestoreSerialized()
  {
    $context = new Restore(new SerializedRestore([]));
    return $context->isEnabled();
  }
}
