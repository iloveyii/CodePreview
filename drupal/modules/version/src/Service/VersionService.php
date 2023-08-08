<?php

/**
 * @file
 * Contains Drupal\version\Service\VersionService
 */

namespace Drupal\version\Service;

use Drupal\Core\Site\Settings;


class VersionService
{
    /**
     * Set the version for a module
     * 
     * @param string $module_name
     * @param string $version
     */
    public function setVersion($module_name, $version)
    {
        $table = 'version';
        $rows = \Drupal::database()->select($table, 'v')
            ->fields('v', ['id'])
            ->condition('module', $module_name)->execute()->fetchCol(0);
        $id = is_array($rows) && (count($rows) > 0) ? $rows[0] : null;

        if (is_null($id)) {
            return \Drupal::database()->insert($table)
                ->fields(array('module' => $module_name, 'version' => $version, 'created' => time()))
                ->execute();
        }

        return \Drupal::database()->update($table)
            ->fields(array('module' => $module_name, 'version' => $version, 'created' => time()))
            ->condition('id', $id)
            ->execute();
    }

    /**
     * Get current version of a module
     */
    public function current($module_name)
    {
        $table = 'version';
        $rows = \Drupal::database()->select($table, 'v')
            ->fields('v', ['version'])
            ->condition('module', $module_name)->execute()->fetchCol(0);
        return is_array($rows) && (count($rows) > 0) ? $rows[0] : \Drupal::config('version.settings')->get('db_version');
    }

    /**
     * Get current version of a env
     */
    public function currentEnv()
    {
        $file_path = \Drupal::service('file_system')->realpath(sprintf("%s/../.env", DRUPAL_ROOT));
        $env = file_get_contents($file_path);
        preg_match('/APP_VERSION=(.*)/i',  $env, $matches);
        if (is_array($matches) && count($matches) == 2) {
            return $matches[1];
        }

        return ('Could not determine version from env file at ' . $file_path);
    }

    /**
     * Get all module names
     */
    public function modules()
    {
        $table = 'version';
        $rows = \Drupal::database()->select($table, 'v')
            ->fields('v', ['module'])
            ->execute()->fetchCol(0);
        if (is_array($rows) && count($rows) > 0) {
            return array_combine($rows, $rows);
        }
        return ['site_version' => 'site_version'];
    }

    /**
     * Save version in db
     * 
     * @param string $version
     */
    public function replaceInDb($version)
    {
        return \Drupal::service('versions.service')->setVersion('site_version', $version);
    }

    /**
     * Save version in env
     * 
     * @param string $version
     */
    public function replaceInEnv($version)
    {
        $file_path = \Drupal::service('file_system')->realpath(sprintf("%s/../.env", DRUPAL_ROOT));
        if (!file_exists($file_path)) {
            return false;
        }
        $env = file_get_contents($file_path);
        $env = preg_replace('/(APP_VERSION=.*)/i', "APP_VERSION=$version", $env, 1);
        return file_put_contents($file_path, $env);
    }

    /**
     * Save version in install.txt
     * 
     * @param string $version
     */
    public function replaceInInstall($version)
    {
        $build_path = Settings::get('build_path');
        $file_path = \Drupal::service('file_system')->realpath(sprintf("%s/install.txt", $build_path));
        if (!file_exists($file_path)) {
            return false;
        }
        $install = file_get_contents($file_path);
        $install = preg_replace('/v\d+.\d+.\d+.\d+/i', "$version", $install);
        return file_put_contents($file_path, $install);
    }
}
