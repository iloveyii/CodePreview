<?php

/**
 * @file
 * @Contains Drupal\repair\Controller\ModelController
 */

namespace Drupal\repair\Controller;

use Drupal\Core\Controller\ControllerBase;
use Symfony\Component\HttpFoundation\JsonResponse;
use Drupal\Component\Datetime\TimeInterface;
use Symfony\Component\DependencyInjection\ContainerInterface;
use Drupal\repair\Model\Model;
use Drupal\repair\Utils\Constant;
use Drupal\repair\Utils\Tax;
use Drupal\taxonomy\Entity\Term;
use Symfony\Component\HttpFoundation\Request;

/**
 * Repair controller
 *
 * This provides the repair page
 */
class ModelController extends ControllerBase
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
  public function models($vendor_id)
  {
    $models = Tax::getTermChildrenByParentId('devices', $vendor_id, 'thumbnail');
    $vendor = Term::load($vendor_id);
    $image_url = Constant::DEFAULT_IMAGE_URL;
    $description = Constant::DEFAULT_VENDOR_DESCRIPTION;
    if (!is_null($vendor)) {
      $image_url = Tax::getTermImageUrl($vendor);
      $description = empty($vendor->description->value) ?  Constant::DEFAULT_VENDOR_DESCRIPTION : $vendor->description->value;
    }
    $search_form = \Drupal::formBuilder()->getForm('Drupal\repair\Form\SearchForm');
    if(count($models) < 2) {
      $search_form = \Drupal::formBuilder()->getForm('Drupal\repair\Form\GeneralForm');
    }
    \Drupal::service('page_cache_kill_switch')->trigger();

    return [
      '#theme' => 'model',
      '#title' => 'Models Page',
      '#description' => $description,
      '#image_url' => $image_url,
      '#search_form' => $search_form,
      '#list_models' => $models,
      '#cache' => [
        'max-age' => 0
      ]
    ];
  }

  /**
   * Autocomplete
   */
  public function autocomplete(Request $request)
  {
    $current_path = \Drupal::service('path.current')->getPath(); //returns /node/1234
    $vendor_id = basename($current_path);
    $q = $request->query->get('q');
    $matches = Model::autocomplete($q, $vendor_id);
    return new JsonResponse($matches);
  }
}
