<?php

/**
 * @file
 * @Contains Drupal\repair\Model\Repairing
 */

namespace Drupal\repair\Model;

use Drupal\node\Entity\Node;
use Drupal\repair\Utils\Constant;
use Drupal\repair\Utils\Helper;
use Drupal\repair\Utils\Tax;

/**
 * Repairing Model
 *
 * This provides the repair page
 */
class Repairing
{
  /**
   * Load all nodes of repairing
   */
  public static function loadMultiple()
  {
    $nodes = \Drupal::entityTypeManager()->getStorage('node')->loadByProperties(['type' => 'repairing']);
    return $nodes;
  }

  /**
   * Get all icons
   */
  public static function icons($node)
  {
    $icons = [];
    if (!$node instanceof Node) {
      return $icons;
    }
    $paragraphs = $node->get('field_icons')->referencedEntities();

    foreach ($paragraphs as $paragraph) {
      $icons[] = [
        'id' => $paragraph->id(),
        'title' => $paragraph->get('field_title')->value,
        'icon' => static::get_paragraph_icon($paragraph),
        'description' => $paragraph->get('field_description_formatted')->value
      ];
    }

    return $icons;
  }

  /**
   * Get icon url in precedence order
   */
  private static function get_paragraph_icon($paragraph)
  {
    $icon = $paragraph->get('field_icon_bootstrap')->value;
    if (empty($icon)) {
      $icon = Helper::getParagraphImageUrl($paragraph, 'field_icon');
    }
    if (empty($icon)) {
      $icon = Constant::DEFAULT_IMAGE_URL;
    }
    return $icon;
  }

  /**
   * Get all features
   */
  public static function features($id)
  {
    $node = Node::load($id);
    $features = [];
    if (!$node instanceof Node) {
      return $features;
    }
    $paragraphs = $node->get('field_features')->referencedEntities();

    foreach ($paragraphs as $paragraph) {
      $features[] = [
        'id' => $paragraph->id(),
        'title' => $paragraph->get('field_title')->value,
        'icon' => $paragraph->get('field_icon_bootstrap')->value,
        'description' => $paragraph->get('field_description_formatted')->value
      ];
    }

    return $features;
  }

  /**
   * Top heading one
   */
  public static function h1($node)
  {
    return $node->field_heading_1->value;
  }

  /**
   * Top heading two
   */
  public static function h2($node)
  {
    return $node->field_heading_2->value;
  }

  /**
   * Get all clients
   */
  public static function clients($id)
  {
    $node = Node::load($id);
    $clients = [];
    if (!$node instanceof Node) {
      return $clients;
    }
    $paragraphs = $node->get('field_clients')->referencedEntities();

    foreach ($paragraphs as $paragraph) {
      $clients[] = [
        'id' => $paragraph->id(),
        'title' => $paragraph->get('field_title')->value,
        'icon' => Helper::getParagraphImageUrl($paragraph, 'field_icon'),
        'icon_bootstrap' => '',
        'description' => $paragraph->get('field_description_formatted')->value
      ];
    }
    return $clients;
  }

  /**
   * Get all areas
   */
  public static function areas($id)
  {
    $node = Node::load($id);
    $areas = [];
    if (!$node instanceof Node) {
      return $areas;
    }
    $paragraphs = $node->get('field_areas')->referencedEntities();

    foreach ($paragraphs as $paragraph) {
      $areas[] = [
        'id' => $paragraph->id(),
        'title' => $paragraph->get('field_title')->value,
        'icon' => Helper::getParagraphImageUrl($paragraph, 'field_icon'),
        'icon_bootstrap' => $paragraph->get('field_icon_bootstrap')->value,
        'description' => $paragraph->get('field_description_formatted')->value
      ];
    }
    return $areas;
  }

  /**
   * Get all faqs
   */
  public static function faq($id)
  {
    $node = Node::load($id);
    $clients = [];
    if (!$node instanceof Node) {
      return $clients;
    }
    $paragraphs = $node->get('field_faq')->referencedEntities();

    foreach ($paragraphs as $paragraph) {
      $clients[] = [
        'id' => $paragraph->id(),
        'title' => $paragraph->get('field_title')->value,
        'icon' => Helper::getParagraphImageUrl($paragraph, 'field_icon'),
        'icon_bootstrap' => $paragraph->get('field_icon_bootstrap')->value,
        'description' => $paragraph->get('field_description_formatted')->value
      ];
    }
    return $clients;
  }
}
