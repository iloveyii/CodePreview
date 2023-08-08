<?php

namespace Drupal\repair\Element;

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
 * $build['models'] = [
 *   '#type' => 'models_list',
 *   '#entity_type' => 'node',
 *   '#entity_id' => 1,
 *   '#view_mode' => 'teaser,
 *   '#langcode' => 'en',
 * ];
 * @endcode
 *
 * @RenderElement("models_list")
 */
class ModelsList extends RenderElement
{

  /**
   * {@inheritdoc}
   */
  public function getInfo()
  {
    return [
      '#theme' => 'models_list',
      '#id' => 'models_list',
      '#title' => 'title',
      '#list_models' => [],
      '#center' => true,
      '#attached' => [
        'library' => [
          'repair/repair.submit'
        ]
      ]
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
