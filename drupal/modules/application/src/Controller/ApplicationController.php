<?php

/**
 * @file
 * @Contains Drupal\application\Controller\ApplicationController
 */

namespace Drupal\application\Controller;

use DateTimeInterface;
use Drupal\Core\Controller\ControllerBase;
use Drupal\node\Entity\Node;
use Drupal\Core\Datetime\DrupalDateTime;
use Drupal\datetime\Plugin\Field\FieldType\DateTimeItemInterface;
use Drupal\Core\Url;


class ApplicationController extends ControllerBase
{

  /**
   * Edit application
   */
  public function view($nid)
  {
    $this->messenger()->addStatus($this->t('You can only view your application as the application deadline has passed.'));
    return $this->edit($nid, false);
  }
  /**
   * Edit application
   */
  public function edit($nid, $edit_mode = true)
  {
    $login_service = \Drupal::service('application.login');
    $content = [];

    // application exists
    if (!isset($nid) && !is_numeric($nid)) {
      echo "Editing applicatin, the id is either not set or not numeric {$nid}";
      exit;
    }
    $node = Node::load($nid);

    if (is_null($node)) {
      echo "The application with id {$nid} cannot be found";
      exit;
    }

    // Check if node is of type application
    if ($node->getType() != 'application_form') {
      echo "This node {$nid} is not an application";
      exit;
    }

    // application challenge exists
    $challenge = Node::load($node->get('field_competition_id')->getString());
    if (is_null($challenge)) {
      echo "The challenged with id {$node->get('field_competition_id')->getString()} cannot be found";
      exit;
    }

    $state_service = \Drupal::service('state.service');
    if ($edit_mode && !$state_service->isChallengeOpened($challenge)) {
      $this->messenger()->addMessage($this->t('This @title is not open for application', ['@title' => $challenge->getTitle()]));
      $form['application_process'] = [
        '#markup' => '<h1>Application process</h1><p>You cannot apply on this content.</p>'
      ];
      return $form;
    }
    // Load form
    $content['form'] = \Drupal::formBuilder()->getForm('Drupal\application\Form\ApplicationForm', $node);

    // Owns application
    if (!$login_service->ownsApplication($node->get('field_uid')->getString())) {
      $content['form'] = [
        '#markup' =>  sprintf("<div class='container'><h2> You cannot edit this application as this is not the application of the logged in user.</h2></div>")
      ];
    }

    // Don't cache this page
    $content['#cache']['max-age'] = 0;
    \Drupal::service('page_cache_kill_switch')->trigger();

    return $content;
  }

  /**
   * Display a success page for any operation
   */
  public function success()
  {
    $link = "<a href='/application/login'> Click to Login</a>";

    // Application form CREATED else UPDATED

    // CREATED
    $link_login = sprintf("<a class='btn btn-primary mt-3 px-3 max-width-200' href='/application/login'> Login </a>");
    $link_index = sprintf("<a class='btn btn-outline-primary mt-3 px-3' href='/'> Home page </a>");
    $msg = 'Your application has been submitted successfully, please click the Login button below if you want to make any changes.';

    // UPDATED
    if (isset($_GET['uid']) && isset($_GET['app']) && $_GET['app'] == 'updated') {
      $url = Url::fromRoute('application.dashboard', ['uid' => $_GET['uid']], ['absolute' => FALSE]);
      $link_login = sprintf("<a class='btn btn-primary mt-3 px-3' href='%s'> Dashboard </a>", $url->toString());
      $msg = 'Your application has been updated successfully, please click the Dashboard button below to see your application.';
    }

    $content['form'] = [
      '#markup' =>  sprintf("<div class='p-xl-5 p-3 mb-3 card-border-none'> <h2> Success !</h2> <p>%s</p> <p> %s %s </p></div>", $msg, $link_login, $link_index)
    ];

    // Don't cache this page
    $content['#cache']['max-age'] = 0;
    \Drupal::service('page_cache_kill_switch')->trigger();
    return $content;
  }
}
