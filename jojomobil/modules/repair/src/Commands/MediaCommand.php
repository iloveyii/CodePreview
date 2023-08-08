<?php

namespace Drupal\repair\Commands;

use Drupal\application\Util\Links;
use Drush\Commands\DrushCommands;
use Symfony\Component\DependencyInjection\ContainerInterface;
use Drupal\Core\Entity\EntityTypeManager;
use Drupal\file\Entity\File;
use Drupal\node\Entity\Node;
use Goutte\Client;
use Solarium\Exception\HttpException;
use Symfony\Component\DomCrawler\Crawler;
use Symfony\Component\DomCrawler\Link;
use Drupal\media\Entity\Media;
use Drupal\Core\File\FileSystemInterface;
use Drupal\media\Entity\MediaType;
use Drupal\repair\Utils\FileHelper;
use Drupal\repair\Utils\MediaHelper;

/**
 * A drush command file
 *
 * @package Drupal\application\Commands
 */
class MediaCommand extends DrushCommands
{
  /**
   * Entity manager service.
   *
   * @var \Drupal\Core\Entity\EntityTypeManagerInterface
   */
  protected $entityTypeManager;

  /**
   * The file system service.
   *
   * @var \Drupal\Core\File\FileSystemInterface
   */
  protected $fileSystem;

  /**
   * The data array.
   *
   * @var array
   */
  protected $data = [];

  /**
   * Construct the DropzoneUploadSave object.
   *
   * @param \Drupal\Core\Entity\EntityTypeManagerInterface $entity_type_manager
   *   Entity type manager service.
   * @param \Drupal\Core\File\FileSystemInterface $file_system
   *   The file system service.
   * @param \Drupal\Core\Logger\LoggerChannelFactoryInterface $logger_factory
   *   The logger factory service.
   * @param \Drupal\Core\Config\ConfigFactoryInterface $config_factory
   *   Config factory service.
   * @param \Drupal\Core\Messenger\MessengerInterface $messenger
   *   The messenger service.
   */
  public function __construct(EntityTypeManager $em, FileSystemInterface $file_system)
  {
    $this->fileSystem = $file_system;
  }

  /**
   * {@inheritdoc}
   */
  public static function create(EntityTypeManager $em, FileSystemInterface $file_system)
  {
    return new static(
      $em,
      $file_system
    );
  }

  /**
   * Drush command that run the code written in it - for testing.
   *
   * @command media:run
   * @aliases media:run d:run
   * @option challenge
   *   Downlaods files of all challenge.
   * @option application_form
   *   Downlaods files of all application forms.
   * @usage media:run --create|--delete --add
   */
  public function run($options = ['create' => FALSE, 'delete' => FALSE, 'add' => FALSE])
  {
    echo sprintf("Running .... %s", PHP_EOL);
    $device = 'data'; // subdir of html/
    $vendor = 'Cell-Phone'; // json filename without ext

    $category = sprintf("%s %s", ucfirst($vendor), ucfirst($device));
    $bundle = $this->create_media_type($category);

    $models = json_decode(file_get_contents($this->make_path($device, $vendor . '.json')), true);
    $models = array_slice($models, 0, 20);

    if ($options['create']) {
      echo 'Create' . PHP_EOL;
      foreach ($models as $model) {
        $url = $model['url'];
        $ext = pathinfo(basename($url), PATHINFO_EXTENSION);
        $name =  $model['name'] . '.' . $ext;
        $image = $this->download_image($url, $device, FileHelper::filify(basename($name)));
        $this->create_media($image, $model['name'], $bundle);
      }
    }

    if ($options['delete']) {
      $id = 645;
      echo 'Delete' . PHP_EOL;
    }

    if ($options['add']) {
      echo 'Add after Create and not Delete' . PHP_EOL;
    }
  }

  private function create_media_type($name)
  {
    $bundle = FileHelper::filify($name, true);

    if (!MediaHelper::existMediaType($bundle)) {
      $values = [
        'id' => $bundle,
        'label' => ucfirst($name),
        'description' => ucfirst($name),
        'source' => 'image',
        'source_configuration' => [
          'source_field' =>  'field_media_image'
        ]
      ];
      MediaHelper::createMediaType($values);
      MediaHelper::media_type_attach_field($bundle, 'Upload image');
    }
    return $bundle;
  }

  private function create_media($image, $title, $bundle)
  {
    $image_media = Media::create([
      'name' => $title,
      'bundle' => $bundle,
      'uid' => 1,
      'langcode' => 'en',
      'status' =>  1,
      'field_media_image' => [
        'target_id' => $image->id(),
        'alt' => t($title),
        'title' => t($title),
      ],
      'field_author' => 'Ali',
      'field_date' => '2025-12-31T23:59:59',
      'field_location' => 'Global',
    ]);
    $image_media->setPublished(TRUE)->save();
  }

  private function download_image($url, $device, $filename)
  {
    $image_data = file_get_contents($url);
    $file_repository = \Drupal::service('file.repository');
    $dir = sprintf("public://images/%s", $device);
    $destination = sprintf("public://images/%s/%s", $device, $filename);
    if (!file_exists($dir)) {
      $this->fileSystem->mkdir($dir, NULL, TRUE);
    }
    $image = $file_repository->writeData($image_data, $destination, FileSystemInterface::EXISTS_REPLACE);
    return $image;
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

  private function get_mobile_page_image_url($url)
  {
    // $page = new Crawler($this->get_file('mobiles', 'iphone.html'));
    $client = new Client();
    $page = $client->request('GET', $url);
    $list = $page->filter('._pdimg')->filter('._psldr')->filter('._pdmimg img')->eq(0)->attr('src');
    $list = explode('?', $list);
    return ($list[0]);
  }
}

// Test run
// docker exec -ti jojomobil_drupal /opt/vendor/drush/drush/drush media:run --create
// https://www.drupal.org/docs/drupal-apis/entity-api/creating-a-custom-content-type-in-drupal-8
