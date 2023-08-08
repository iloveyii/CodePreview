<?php

namespace Drupal\actioner\Plugin\RulesAction;

use Drupal\Core\Entity\EntityInterface;
use Drupal\rules\Core\RulesActionBase;

/**
 * Provides a 'Delete entity' action.
 *
 * @RulesAction(
 *   id = "rules_entity_delete_custom",
 *   label = @Translation("Delete entity custom"),
 *   category = @Translation("Entity"),
 *   context_definitions = {
 *     "entity" = @ContextDefinition("entity",
 *       label = @Translation("Entity"),
 *       description = @Translation("Specifies the entity, which should be deleted permanently.")
 *     )
 *   }
 * )
 */
class EntityDelete extends RulesActionBase
{

    /**
     * Deletes the Entity.
     *
     * @param \Drupal\Core\Entity\EntityInterface $entity
     *    The entity to be deleted.
     */
    protected function doExecute(EntityInterface $entity)
    {
        \Drupal::messenger()->addStatus(t('Node with ID: %id is deleted !', ['%id' => $entity->id()]));
        $entity->delete();
    }
}
