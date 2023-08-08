<?php

/**
 * @file
 * Contains Drupal\application\Form\LoginForm
 */

namespace Drupal\application\Form;

use Drupal\Core\Form\FormBase;
use Drupal\Core\Form\FormStateInterface;
use Drupal\node\Entity\Node;
use Drupal\symfony_mailer\EmailFactoryInterface;
use Drupal\symfony_mailer\MailerHelperInterface;
use Symfony\Component\DependencyInjection\ContainerInterface;
use Drupal\Core\Url;


/**
 * Create Reset Form
 */
class ResetForm extends FormBase
{
  /**
   * The email factory service.
   *
   * @var \Drupal\symfony_mailer\EmailFactoryInterface
   */
  protected $emailFactory;

  /**
   * The mailer helper.
   *
   * @var \Drupal\symfony_mailer\MailerHelperInterface
   */
  protected $helper;

  /**
   * Constructs a new TestForm.
   *
   * @param \Drupal\symfony_mailer\EmailFactoryInterface $email_factory
   *   The email factory service.
   * @param \Drupal\symfony_mailer\MailerHelperInterface $helper
   *   The mailer helper.
   */
  public function __construct(EmailFactoryInterface $email_factory, MailerHelperInterface $helper)
  {
    $this->emailFactory = $email_factory;
    $this->helper = $helper;
  }

  /**
   * {@inheritdoc}
   */
  public static function create(ContainerInterface $container)
  {
    return new static(
      $container->get('email_factory'),
      $container->get('symfony_mailer.helper'),
    );
  }
  /**
   * {@inheritdoc}
   */
  public function getFormId()
  {
    return 'application_login_reset_form';
  }

  /**
   * {@inheritdoc}
   */
  public function buildForm(array $form, FormStateInterface $form_state)
  {
    // Form
    $row['email'] = $form_state->getValue('email');

    $form['form']['email'] = [
      '#type' => 'textfield',
      '#size' => 75,
      '#maxlength' => 70,
      '#placeholder' => t('Type your email'),
      '#required' => true,
      '#default_value' => isset($row['email']) ? $row['email'] : '',
    ];

    $form['#theme'] = 'reset';

    $form['form']['actions']['submit'] = [
      '#type' => 'submit',
      '#value' => $this->t('Send me email'),
      '#attributes' => array('class' => array('btn btn-primary btn-sm', 'mt-2', 'px-3', 'me-1')),
    ];
    $form['form']['actions']['reset'] = [
      '#type' => 'link',
      '#title' => $this->t('Login'),
      '#url' => Url::fromRoute('application.login'),
      '#attributes' => array('class' => array('btn btn-outline-secondary btn-md', 'mt-2', 'px-3')),
    ];

    return $form;
  }

  /**
   * {@inheritdoc}
   */
  public function validateForm(array &$form, FormStateInterface $form_state)
  {
    $email = $form_state->getValue('email', $_POST['email']);

    // Validate email
    if (empty($email) || !\Drupal::service('email.validator')->isValid($email)) {
      $form_state->setErrorByName('email', $this->t('The email [@email] is not valid.', ['@email' => $email]));
    }
  }

  /**
   * {@inheritdoc}
   */
  public function submitForm(array &$form, FormStateInterface $form_state)
  {
    $to = trim($form_state->getValue('email', $_POST['email']));
    $app_node = \Drupal::service('application.node');
    $uids = $app_node->getUids($to);

    $sendEmail = false;
    $c_service = \Drupal::service('competition.service');

    $competition = $c_service->getLatestCompetition();
    if ($competition instanceof Node) {
      $sendEmail = $competition->hasField('field_send_email') ? $competition->get('field_send_email')->getString() : null;
    }

    $email = $this->emailFactory->newTypedEmail('symfony_mailer', 'test');
    $email->setTo($to);
    $email->setVariable('uids', $uids);
    $email->setVariable('site_url', sprintf("%s://%s:%d", \Drupal::request()->getScheme(), \Drupal::request()->getHost(), \Drupal::request()->getPort()));

    if ($sendEmail) {
      $email->send();
      $message = is_object($to) ?
        $this->t('An attempt has been made to send an email to you.') :
        $this->t('An attempt has been made to send an email to @to.', ['@to' => $to]);
      $this->messenger()->addMessage($message);
    }
  }
}
