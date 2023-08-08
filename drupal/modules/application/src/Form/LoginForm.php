<?php

/**
 * @file
 * Contains Drupal\application\Form\LoginForm
 */

namespace Drupal\application\Form;

use Drupal\Core\Form\FormBase;
use Drupal\Core\Form\FormStateInterface;
use Drupal\Core\Url;
use Drupal\node\Entity\Node;
use Exception;

/**
 * Create Application Form
 */
class LoginForm extends FormBase
{
  /**
   * {@inheritdoc}
   */
  public function getFormId()
  {
    return 'application_login_form';
  }

  /**
   * {@inheritdoc}
   */
  public function buildForm(array $form, FormStateInterface $form_state)
  {
    // Form
    $row['uid'] = $form_state->getValue('uid');
    $row['code'] = $form_state->getValue('code');

    $form['uid'] = [
      '#type' => 'textfield',
      '#size' => 25,
      '#maxlength' => 15,
      '#placeholder' => t('UID'),
      '#required' => true,
      '#default_value' => isset($row['uid']) ? $row['uid'] : '',
    ];
    $form['code'] = [
      '#type' => 'textfield',
      '#size' => 25,
      '#maxlength' => 15,
      '#placeholder' => t('Code'),
      '#required' => true,
      '#default_value' => isset($row['code']) ? $row['code'] : '',
    ];
    $form['#theme'] = 'login';

    $form['actions']['submit'] = [
      '#type' => 'submit',
      '#value' => $this->t('Login'),
      '#attributes' => array('class' => array('btn btn-primary btn-sm', 'mt-2', 'px-3', 'me-1')),
    ];

    $form['actions']['reset'] = [
      '#type' => 'link',
      '#title' => $this->t('Forgot ?'),
      '#url' => Url::fromRoute('application.reset'),
      '#attributes' => array('class' => array('btn btn-outline-primary btn-md', 'mt-2', 'px-3')),
    ];

    $form['actions']['space'] = [
      '#markup' => '<div class="d-block mb-5"></div>',
    ];

    return $form;
  }

  /**
   * {@inheritdoc}
   */
  public function validateForm(array &$form, FormStateInterface $form_state)
  {
    parent::validateForm($form, $form_state);

    // Validate uid
    $uid = trim($form_state->getValue('uid'));
    if ($uid && strlen($uid) < 3) {
      $form_state->setErrorByName('uid', $this->t('The uid [@uid] is too short. Please enter a full uid.', ['@uid' => $uid]));
    }

    // Validate code
    $code = trim($form_state->getValue('code'));
    if ($code && strlen($code) < 3) {
      $form_state->setErrorByName('code', $this->t('The code [@code] is too short. Please enter a full code.', ['@code' => $code]));
    }

    $login_service = \Drupal::service('application.login');

    // Find node, and fetch uid and code
    if (!$login_service->userCanLogin($uid, $code)) {
      $email = $this->config('application.settings')->get('email');
      $this->messenger()->addStatus($this->t('Your uid: @uid or code is incorrect, please contact support at @email', ['@uid' => $form_state->getValue('uid'), '@email' => $email]));

      $form_state->setErrorByName('uid', $this->t('The uid [@uid] or code is invalid', ['@uid' => $uid]));
      $form_state->setErrorByName('code', $this->t('The uid or code [@code] is invalid', ['@code' => $code]));
    }
  }

  /**
   * {@inheritdoc}
   */
  public function submitForm(array &$form, FormStateInterface $form_state)
  {
    $review_is_open = false;
    $cid = '';

    // To which competition the user belongs
    $uid = trim($form_state->getValue('uid'));
    $code = trim($form_state->getValue('code'));

    $login_service = \Drupal::service('application.login');
    $result = $login_service->existUidCode($uid, $code);

    if (is_array($result) && isset($result['cid'])) {
      $cid = $result['cid'];
    }

    if (empty($cid)) {
      $form_state->setRedirect('application.reviewer');
      throw new Exception("CID cannot be empty for login user {$cid}");
      return;
    }

    $form_state->setRedirect('application.dashboard', ['uid' => $uid]);
  }
}
