<?php

/**
 * @file
 * @Contains Drupal\repair\Controller\RepairController
 */

namespace Drupal\repair\Controller;

use Drupal\Core\Controller\ControllerBase;
use Drupal\Core\Link;
use Drupal\Core\Url;
use Drupal\node\Entity\Node;
use Drupal\swish\Model\SwishPayment;
use Symfony\Component\HttpFoundation\JsonResponse;
use Drupal\Component\Datetime\TimeInterface;
use Symfony\Component\DependencyInjection\ContainerInterface;
use Drupal\commerce_order\Entity\Order;
use Drupal\commerce_checkout\Event\CheckoutEvents;
use Drupal\commerce_order\Event\OrderEvent;
use Drupal\field\Entity\FieldConfig;
use Drupal\field\Entity\FieldStorageConfig;
use Drupal\repair\Model\Repairing;
use Drupal\repair\Utils\Constant;
use Drupal\repair\Utils\FileHelper;
use Drupal\repair\Utils\Helper;
use Drupal\repair\Utils\MediaHelper;
use Drupal\repair\Utils\Tax;

/**
 * Repair controller
 *
 * This provides the repair page
 */
class RepairController extends ControllerBase
{
  /**
   * The time.
   *
   * @var \Drupal\Component\Datetime\TimeInterface
   */
  protected $time;

  public function __construct(TimeInterface $time)
  {
    $this->time = $time;
  }


  /**
   * {@inheritdoc}
   */
  public static function create(ContainerInterface $container)
  {
    return new static(
      $container->get('datetime.time'),
    );
  }

  /**
   * The page
   */
  public function repair()
  {
    $list_devices = Tax::getTermChildrenByParentId('devices', 0, 'thumbnail');
    $node = Node::load(Constant::DEFAULT_REPAIRING_NODE_ID);
    $h1 =  Repairing::h1($node);
    $h2 =  Repairing::h2($node);
    $list_icons =  Repairing::icons($node);
    $list_features =  Repairing::features(Constant::DEFAULT_REPAIRING_NODE_ID);
    $list_clients =  Repairing::clients(Constant::DEFAULT_REPAIRING_NODE_ID);
    $list_areas =  Repairing::areas(Constant::DEFAULT_REPAIRING_NODE_ID);
    $list_faq =  Repairing::faq(Constant::DEFAULT_REPAIRING_NODE_ID);
    $shop_image = Helper::getNodeImageUrl($node);

    \Drupal::service('page_cache_kill_switch')->trigger();
    return [
      '#theme' => 'repair',
      '#title' => 'Repairing Page',
      '#h1' => $h1,
      '#h2' => $h2,
      '#list_icons' => $list_icons,
      '#list_devices' => $list_devices,
      '#list_features' => $list_features,
      '#list_clients' => $list_clients,
      '#list_areas' => $list_areas,
      '#list_faq' => $list_faq,
      '#shop_image' => $shop_image,
      '#cache' => [
        'max-age' => 0
      ],
      '#attached' => [
        'library' => [
          'repair/repair.clients',
          'repair/repair.reviews',
        ]
      ]
    ];
  }

  private function media()
  {
    // FROM HERE
    $field_storage = FieldStorageConfig::loadByName('media', 'field_media_image');
    $field = FieldConfig::loadByName('media', 'image', 'field_media_image');
    if (empty($field)) {
      $field = FieldConfig::create([
        'field_storage' => $field_storage,
        'bundle' => 'media',
        'label' => 'a label',
        'settings' => ['display_summary' => true],
      ]);
      $field->save();
    }
    /** @var \Drupal\Core\Entity\EntityDisplayRepositoryInterface $display_repository */
    $display_repository = \Drupal::service('entity_display.repository');

    // Assign widget settings for the default form mode.
    $display_repository->getFormDisplay('media', 'image')
      ->setComponent('field_media_image', [
        'type' => 'image_image',
      ])
      ->save();

    // Assign display settings for the 'default' and 'teaser' view modes.
    $display_repository->getViewDisplay('media', 'image')
      ->setComponent('field_media_image', [
        'label' => 'hidden',
        'type' => 'image',
      ])
      ->save();

    // The teaser view mode is created by the Standard profile and therefore
    // might not exist.
    $view_modes = \Drupal::service('entity_display.repository')->getViewModes('media');
    if (isset($view_modes['teaser'])) {
      $display_repository->getViewDisplay('media', 'image', 'teaser')
        ->setComponent('field_media_image', [
          'label' => 'hidden',
          'type' => 'image',
        ])
        ->save();
    }

    return $field;
  }
}
