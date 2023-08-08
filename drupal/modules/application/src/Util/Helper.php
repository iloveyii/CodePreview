<?php

/**
 * @file
 * @Contains Drupal\application\Util\Helper
 */

namespace Drupal\application\Util;

use Drupal\Core\Controller\ControllerBase;
use Symfony\Component\DependencyInjection\ContainerInterface;
use Drupal\Core\Form\FormStateInterface;
use Drupal\node\Entity\Node;
use Drupal\field\Entity\FieldConfig;



class Helper
{

  /**
   * String to url
   * Replace non-alphanumeric by _ & double spaces by single & single space by - from competition number, lowercase
   *
   * @return string
   */
  public static function urlify(string $str): string
  {
    $str = strtolower(trim($str));
    $str = preg_replace('!\s+!', '-', $str);
    $str = preg_replace('/[^a-z0-9-]/i', '_', $str);
    return $str;
  }

  /**
   * String remove non alpha underscore dot
   */
  public static function filify($str)
  {
    $str = preg_replace('/[^\da-z_.]/i', '-', strtolower($str));
    $str = str_replace('--', '-', $str);
    return $str;
  }
  /**
   * Get term id by name in a vocabulary
   */
  public static function getTermIdByName($vid, $term_name)
  {
    $terms = \Drupal::entityTypeManager()->getStorage('taxonomy_term')->loadTree($vid);

    foreach ($terms as $term) {
      if (strtolower($term->name) == strtolower(trim($term_name))) {
        return $term->tid;
      }
    }

    return false;
  }

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
    if ($node->hadField($field)) {
      return t($node->get($field)->getFieldDefinition()->getLabel());
    }
    return false;
  }

  /**
   * Logger
   */
  public static function logger($msg)
  {
    \Drupal::logger('application')->notice($msg);
  }


  /**
   * Rename uploading compound file
   */
  public static function rename_compound_file($id, $challenge_no, $section, $file_name)
  {
    // max file name len without ext
    $max = 200;
    $file_name = Helper::filify($file_name);
    $date = date("Y-m-d_H-i-s", time());
    // Get ext
    $ext = pathinfo($file_name, PATHINFO_EXTENSION);
    $file_name_no_ext =   rtrim($file_name, '.' . $ext);

    // Remove chars from right
    if (strlen($file_name_no_ext) > $max) {
      $file_name = substr($file_name, strlen($file_name_no_ext) - $max);
    }

    return sprintf("%d-%s-%s-%s-%s", $id, $challenge_no, $section, $date, $file_name);
  }

  /**
   * Get links for a node
   */
  private function node_links($node, $fields)
  {
    $node_links = [];
    foreach ($fields as $field) {
      if (!empty($node->{$field}->entity)) {
        foreach ($node->{$field} as $file) {
          $node_links[$field][] = $file->entity->createFileUrl(false);;
        }
      }
    }
  }

  /**
   * Get Links
   */
  public function get_links($bundle = 'challenge')
  {
    $fields = $this->get_node_file_fields($bundle);
    $links = [];
    $nodes = \Drupal::entityTypeManager()->getStorage('node')->loadByProperties(['type' => $bundle]);

    foreach ($nodes as $node) {
      $links[$node->id()] = $this->node_links($node, $fields);
    }
  }

  /**
   * Get node fields
   */
  private function get_node_file_fields($bundle = 'challenge')
  {
    $entity_type_id = 'node';
    $entityFieldManager = \Drupal::service('entity_field.manager');
    $fields = $entityFieldManager->getFieldDefinitions($entity_type_id, $bundle);
    $field_names = [];
    foreach ($fields as $field_name => $field_definition) {
      if (!empty($field_definition->getTargetBundle())) {
        if ($field_definition->getType() == 'file' || $field_definition->getType() == 'image') {
          // $field_definition->getType(); $field_definition->getLabel(); $field_name;
          $field_names[] = $field_definition->getName();
        }
      }
    }
    return ($field_names);
  }
}
