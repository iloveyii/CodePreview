<?php

namespace Drupal\repair\Commands;

use Drupal\application\Util\Links;
use Drush\Commands\DrushCommands;
use Symfony\Component\DependencyInjection\ContainerInterface;
use Drupal\Core\Entity\EntityTypeManager;
use Drupal\file\Entity\File;
use Drupal\node\Entity\Node;
use Drupal\repair\Utils\FileHelper;
use Drupal\repair\Utils\MediaHelper;
use Drupal\repair\Utils\TermHelper;
use Drupal\taxonomy\Entity\Term;
use Goutte\Client;
use Solarium\Exception\HttpException;
use Symfony\Component\DomCrawler\Crawler;
use Symfony\Component\DomCrawler\Link;

/**
 * A drush command file
 *
 * @package Drupal\application\Commands
 */
class TaxCommand extends DrushCommands
{
  protected $container;
  protected $page;
  protected $data = [];

  public function __construct(EntityTypeManager $em, $container = null)
  {
    $this->container = $container;
  }

  /**
   * {@inheritdoc}
   */
  public static function create(EntityTypeManager $em, ContainerInterface $container)
  {
    return new static(
      $em,
      $container
    );
  }

  /**
   * Drush command that run the code written in it - for testing.
   *
   * @command tax:run
   * @aliases tax:run
   * @option challenge
   *   Downlaods files of all challenge.
   * @option application_form
   *   Downlaods files of all application forms.
   * @usage tax:run --create|--delete --add
   */
  public function run($options = ['create' => FALSE, 'delete' => FALSE, 'add' => FALSE])
  {
    echo sprintf("Running .... %s", PHP_EOL);
  }

  /**
   * Drush command that creates all the terms.
   *
   * @command tax:terms.create
   * @aliases tax:v.s
   * @option challenge
   *   Downlaods files of all challenge.
   * @option application_form
   *   Downlaods files of all application forms.
   * @usage tax:terms.create --create|--device|--vendor --add
   */
  public function terms($options = ['create' => FALSE, 'device' => FALSE, 'vendor' => FALSE, 'add' => FALSE])
  {
    echo sprintf("Running .... terms %s", PHP_EOL);
    $device = 'data'; // subdir of html/
    $device_label = 'Data Recovery'; // parent taxonomy term/
    $vendor = 'Cell-Phone'; // json filename without ext
    $models = json_decode(file_get_contents($this->make_path($device, $vendor . '.json')), true);
    $models = array_slice($models, 0, 20);
    $category = sprintf("%s %s", ucfirst($vendor), ucfirst($device));
    $bundle = FileHelper::filify($category, true);

    if ($options['create']) {
      echo 'Create' . PHP_EOL;
      $device_id = TermHelper::device_id_by_name($device_label);
      $vendor_id = TermHelper::vendor_id_by_name($device_label,  $vendor);
      if (is_null($vendor_id)) {
        $term = TermHelper::create_term('devices', ucwords($vendor), $device_id);
        $vendor_id = $term->tid->value;
      }
      foreach ($models as $model) {
        $media = MediaHelper::find_media_by_name($bundle, $model['name']);
        $mid = is_null($media) ? 191 : $media->mid->value;
        $term = TermHelper::create_term('devices', ucwords($model['name']), $vendor_id, $mid);
      }
    }

    if ($options['device']) {
      echo 'Device' . PHP_EOL;
      $device_id = TermHelper::device_id_by_name('Tablett');
      echo ($device_id);
    }

    if ($options['vendor']) {
      echo 'Vendor' . PHP_EOL;
      $vendor_id = TermHelper::vendor_id_by_name($device_label,  'Apple');
      print_r($vendor_id);
    }

    if ($options['add']) {
      echo 'Add' . PHP_EOL;
      $term_id = TermHelper::create_term('devices', 'New Vendor 2', 169);
      $term = Term::load($term_id);
      // $term->parent = 169;
      $term->save();
      print_r($vendor_id);
    }
  }

  private function make_path($subdir, $filename)
  {
    $mod_path = \Drupal::service('extension.list.module')->getPath('repair');
    $full_path = sprintf("%s/html/%s/%s", $mod_path, $subdir, $filename);
    return $full_path;
  }

  private function get_file($subdir, $filename)
  {
    $full_path = $this->make_path($subdir, $filename);
    if (file_exists($full_path)) {
      return file_get_contents($full_path);
    }
    throw new HttpException(404, 'File not found at ' . $full_path);
  }

  private function save_file($subdir, $filename, $content)
  {
    $full_path = $this->make_path($subdir, $filename);
    return file_put_contents($full_path, $content);
  }
}


// Test run
// docker exec -ti jojomobil_drupal /opt/vendor/drush/drush/drush tax:terms.create --create
