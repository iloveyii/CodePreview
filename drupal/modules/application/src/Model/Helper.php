<?php

/**
 * @file
 * Contains Drupal\application\Model\Helper
 */

namespace Drupal\application\Model;

use Drupal\Core\Form\FormStateInterface;
use Drupal\node\Entity\Node;
use Drupal\field\Entity\FieldConfig;

class Helper
{
  /**
   * Read vocabulary
   */
  public static function getTerms($vid)
  {
    $terms = \Drupal::entityTypeManager()->getStorage('taxonomy_term')->loadByProperties(['vid' => $vid]);
    $list_terms = [];
    foreach ($terms as $term) {
      $list_terms[$term->id()] = $term->getName();
    }
    return $list_terms;
  }

  /**
   * Get options from field terms
   */
  public static function getOptions($array)
  {
    $options = [];
    foreach ($array as $item) {
      if (isset($item['target_id'])) {
        $options[] = $item['target_id'];
      }
    }
    return $options;
  }

  /**
   * Get all fields
   */
  public static function getFields($bundle, $all = false)
  {
    $em = \Drupal::service('entity_field.manager');
    $definitions = $em->getFieldDefinitions('node', $bundle);

    foreach ($definitions as $field_name => $definition) {
      if (!$definition instanceof FieldConfig) {
        continue;
      }
      $options[$field_name] = $definition->getLabel();
    }
    return $options;
  }

  /**
   * Get field label
   */
  public static function getLabel($node, $field)
  {
    if ($node->hasField($field)) {
      return t($node->get($field)->getFieldDefinition()->getLabel());
    }
    return false;
  }

  /**
   * Logger
   */
  public static function logger($msg)
  {
    \Drupal::logger('application_form')->notice($msg);
  }
}
