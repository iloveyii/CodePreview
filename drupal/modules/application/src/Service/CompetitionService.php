<?php

/**
 * @file
 * Contains Drupal\application\Service\CompetitionService
 */

namespace Drupal\application\Service;

use Drupal\application\Util\Helper;
use Drupal\node\Entity\Node;
use Drupal\Core\Datetime\DrupalDateTime;
use Drupal\datetime\Plugin\Field\FieldType\DateTimeItemInterface;
use Exception;


class CompetitionService
{

  const STATES = [
    "Draft",
    "Unpublished",
    "Published",
    "Announcement",
    "Application Opens",
    "Application Closes",
    "Review Opens",
    "Review Closed",
    "Panel Review Opens",
    "HI - Upload compound files",
    "MS - Upload compound files",
    "HO - Upload compound files",
    "Make Applications public",
    "Close Challenge"
  ];

  /**
   * Constructor
   */
  public function __construct()
  {
  }

  /**
   * Get all competitions
   */
  public function getAll()
  {
    $entities = \Drupal::entityTypeManager()->getStorage('node')->loadByProperties(['type' => 'challenge']);
    return $entities;
  }

  /**
   * Get all competition numbers formatted
   * @return string
   */
  public function getAllFormattedCompetitionNumbers()
  {
    $entities = $this->getAll();
    $competition_numbers = [];
    foreach ($entities as $entity) {
      if ($entity instanceof Node) {
        $title = trim($entity->getTitle());
        $competition_numbers[] = $this->getFormattedCompetitonNumber($title);
      }
    }
    return $competition_numbers;
  }

  /**
   * Replace non-alphanumeric by _ from competition number, lowercase
   * @return string
   */
  public function getFormattedCompetitonNumber($competition_number)
  {
    return preg_replace('/[^a-z0-9]/i', '_', strtolower($competition_number));
  }

  /**
   * Replace non-alphanumeric by _ from competition number, lowercase
   * @return string
   */
  public function getFormattedCompetitonNumberById($nid)
  {
    $competition_number = null;
    $node = Node::load($nid);
    if ($node instanceof Node) {
      $competition_number = $node->getTitle();
    }
    return preg_replace('/[^a-z0-9]/i', '_', strtolower($competition_number));
  }

  /**
   * If application is opened
   */
  public function applicationsOpened($node)
  {
    if (is_numeric($node)) {
      $node = Node::load($node);
    }
    if (!$node instanceof Node) {
      throw new Exception("Neither a node nor a node id given in service Competition");
    }

    $opens = $node->get('field_application_opens')->date->getTimestamp();
    $closes = $node->get('field_application_closes')->date->getTimestamp();
    $now = new DrupalDateTime('now');
    $now->setTimezone(new \DateTimeZone(DateTimeItemInterface::STORAGE_TIMEZONE));
    $now = $now->getTimestamp();
    $opened = false;

    // Criteria #1 : application is opened and closing time is not passed
    if (($opens < $now) && ($now < $closes)) {
      $opened = true;
    }

    // Criteria #2 : if 'Enable Application for this node' is checked
    // Updated with workflow
    $enabler = \Drupal::service('application.enabler');
    if (!$enabler->isEnabled($node->id())) {
      $opened = $opened && true;
    }

    return $opened;
  }


  /**
   * Get the moderation state
   */
  public static function getState($node)
  {
    $storage = \Drupal::entityTypeManager()->getStorage($node->getEntityTypeId());
    $get_latest_revision_id = $storage->getLatestRevisionId($node->id());
    $get_latest_revision_entity = $storage->loadRevision($get_latest_revision_id);

    if (!empty($get_latest_revision_entity)) {
      $node = $get_latest_revision_entity;
    }
    if (!empty($node->moderation_state->value)) {
      $moderation_information = \Drupal::getContainer()->get('content_moderation.moderation_information');
      $current_state = $moderation_information->getWorkflowForEntity($node)->getTypePlugin()->getState($node->moderation_state->value)->label();
      return trim($current_state); // $node->moderation_state->value; // prints review_opens
    } else if ($node->isPublished()) {
      return ('Published');
    } else {
      return ('Unpublished');
    }

    return NULL;
  }

  /**
   * State greater than published
   */
  public static function stateGreaterThanPublished($node)
  {
    $state = self::getState($node);

    return in_array($state, [
      "Announcement",
      "Application Opens",
      "Application Closes",
      "Review Opens",
      "Review Closed",
      "Panel Review Opens",
      "HI - Upload compound files",
      "MS - Upload compound files",
      "HO - Upload compound files",
      "Make Applications public",
    ]);
  }

  /**
   * If state is a specific state
   *
   * @param string $state
   * @param Node $node
   *
   * {@return boolean}
   */
  public static function isState($state, $node)
  {
    $_state = self::getState($node);
    return trim($state) == $_state;
  }

  public function currentState($node, $return)
  {
    return StateService::currentState($node, $return);
  }

  public function getLatestCompetition()
  {
    $ids = \Drupal::entityQuery('node')
      ->condition('type', 'challenge')
      ->sort('nid', 'DESC')
      ->execute();

    $id = null;
    if (is_array($ids) && (count($ids) > 0)) {
      $id = reset($ids);
    } else {
      return null;
    }

    $node = Node::load($id);
    $state_service = \Drupal::service('state.service');
    $node = $state_service->getLatestVersion($node);

    return $node;
  }

  public static function getUrl($competition)
  {
    return sprintf("/challenges/%s", Helper::urlify($competition->get('field_challenge_title')->getString()));
  }
}
