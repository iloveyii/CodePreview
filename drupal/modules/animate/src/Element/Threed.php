<?php

namespace Drupal\animate\Element;

use Drupal\Core\Render\Element\RenderElement;

/**
 * Provides a render element to display an entity.
 *
 * Properties:
 * - #entity_type: The entity type.
 * - #entity_id: The entity ID.
 * - #view_mode: The view mode that should be used to render the entity.
 * - #langcode: For which language the entity should be rendered.
 *
 * Usage Example:
 * @code
 * $build['node'] = [
 *   '#type' => 'threed',
 *   '#entity_type' => 'node',
 *   '#entity_id' => 1,
 *   '#view_mode' => 'teaser,
 *   '#langcode' => 'en',
 * ];
 * @endcode
 *
 * @RenderElement("threed")
 */
class Threed extends RenderElement
{

  /**
   * {@inheritdoc}
   */
  public function getInfo()
  {
    return [
      // '#pre_render' => [
      //   [get_class($this), 'preRenderEntityElement'],
      // ],
      '#theme' => 'threed',
      '#view_mode' => 'full',
      '#langcode' => NULL,
      '#size' => 'md', //xxl, xl, lg, md, sm, xs
      '#text' => 'Soft\a    Hem!',
      '#link' => '/backend',
      '#bg' => '',
      '#attributes' => NULL
    ];
  }

  /**
   * Entity element pre render callback.
   *
   * @param array $element
   *   An associative array containing the properties of the entity element.
   *
   * @return array
   *   The modified element.
   */
  public static function preRenderEntityElement(array $element)
  {

    $entity_type_manager = \Drupal::entityTypeManager();

    $entity = $entity_type_manager
      ->getStorage($element['#entity_type'])
      ->load($element['#entity_id']);

    if ($entity && $entity->access('view')) {
      $element['entity'] = $entity_type_manager
        ->getViewBuilder($element['#entity_type'])
        ->view($entity, $element['#view_mode'], $element['#langcode']);
    }

    return $element;
  }
}
