<?php

/**
 * @file
 * @Contains Drupal\repair\Term\Model
 */

namespace Drupal\repair\Term;

use Drupal\node\Entity\Node;
use Drupal\repair\Utils\Tax;
use Drupal\taxonomy\Entity\Term;
use Symfony\Component\HttpKernel\Exception\HttpException;

/**
 * Class Model for Term
 *
 * This provides the repair page
 */
class Model extends Base
{
  /**
   * The page
   */
  public function view()
  {
  }

  /**
   * Load all nodes of services
   */
  public static function loadMultiple()
  {
    // @todo for taxonomy
  }

  /**
   * Load Term by id
   */
  public static function load($id)
  {
    $term = Term::load($id);
    if (is_null($term)) {
      throw new HttpException(503, sprintf("Cannot load TERM for id %d", $id));
    }
    return $term;
  }

  /**
   * Get Term array
   */
  public static function loadTermArray($model_id)
  {
    $model = Model::load($model_id);
    $taxo = [
      'title' => $model->getName(),
      'image_url' =>  Tax::getTermImageUrl($model),
      'description' => $model->getDescription()
    ];
    return $taxo;
  }
}
