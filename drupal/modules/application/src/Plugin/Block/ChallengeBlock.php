<?php

/**
 * @file
 * contains Drupal\application\Plugin\Block\ChallengeBlock
 */

namespace Drupal\application\Plugin\Block;

use Drupal\application\Model\Dashboard;
use Drupal\Core\Block\BlockBase;
use Drupal\Core\Session\AccountInterface;
use Drupal\Core\Access\AccessResult;
use Drupal\application\Service\StateService;
use Drupal\application\Util\Helper;


/**
 * Provides block for challenges
 * @Block(
 *  id = "challenges_block",
 *  admin_label = @Translation("List of challenges block")
 * )
 */

class ChallengeBlock extends BlockBase
{
  /**
   * {@inheritdoc}
   */
  public function build()
  {
    $competition_service = \Drupal::service('competition.service');
    $state_service = \Drupal::service('state.service');

    $challenges = $competition_service->getAll();
    krsort($challenges);
    $rows = [];

    foreach ($challenges as $challenge) {
      $challenge = $state_service->getLatestVersion($challenge);
      $current_state = $state_service->getStateId($challenge);
      $state = $state_service->stateIsBetween('announcement', 'close_challenge', $current_state);
      if ($state === false) {
        continue;
      }

      if (!$challenge->isPromoted()) {
        continue;
      }


      $rows[] = [
        'title' => $challenge->getTitle(),
        'url' => sprintf("/challenges/%s", Helper::urlify($challenge->get('field_challenge_title')->getString())),
        'image' => $challenge->field_images->view('teaser'),
        'challenge_title' => $challenge->field_challenge_title->view('default'),
        'description' => $challenge->field_description->view('default'),
        'summary' => $challenge->field_summary->view('default'),
        'application_closes' =>  $challenge->field_application_closes->view('default'),
        'application_is_opened' => $state_service->challengeOpened($challenge) == StateService::OPENED
      ];
    }

    \Drupal::service('page_cache_kill_switch')->trigger();

    return [
      '#theme' => 'challenge_block',
      '#rows' => $rows,
      '#cache' => [
        'max-age' => 0
      ]
    ];
  }

  /**
   * {@inheritdoc}
   */
  public function blockAccess(AccountInterface $account)
  {
    return AccessResult::allowedIfHasPermission($account, 'view application');
  }
}
