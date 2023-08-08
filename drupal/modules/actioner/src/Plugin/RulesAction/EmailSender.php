<?php

namespace Drupal\actioner\Plugin\RulesAction;

use Drupal\Core\Entity\EntityInterface;
use Drupal\rules\Core\RulesActionBase;

/**
 * Provides a 'Delete entity' action.
 *
 * @RulesAction(
 *   id = "actioner_email_sender",
 *   label = @Translation("Send email from symfony mailer"),
 *   category = @Translation("System"),
 *   context_definitions = {
 *     "entity" = @ContextDefinition("entity",
 *       label = @Translation("Entity"),
 *       description = @Translation("Specifies the entity, which should be deleted permanently.")
 *     )
 *   }
 * )
 */
class EmailSender extends RulesActionBase
{

    /**
     * Deletes the Entity.
     *
     * @param \Drupal\Core\Entity\EntityInterface $entity
     *    The entity to be deleted.
     */
    protected function doExecute(EntityInterface $entity)
    {
        \Drupal::messenger()->addStatus(t('Email has been sent to Node with ID: %id !', ['%id' => $entity->id()]));
    }
}
