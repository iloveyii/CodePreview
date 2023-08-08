<?php

namespace Drupal\repair\Form;

use Drupal\Core\Form\FormBase;
use Drupal\Core\Form\FormStateInterface;
use Drupal\node\Entity\Node;
use Drupal\Core\Ajax\AjaxResponse;
use Drupal\Core\Ajax\HtmlCommand;
use Drupal\Core\Render\Renderer;
use Drupal\repair\Model\Model;
use Drupal\repair\Utils\Tax;

/**
 * Provides a Search form.
 */
class SearchForm extends FormBase
{

  /**
   * {@inheritdoc}
   */
  public function getFormId()
  {
    return 'taxonomy_searching_form';
  }

  /**
   * {@inheritdoc}
   */
  public function buildForm(array $form, FormStateInterface $form_state)
  {
    $current_path = \Drupal::service('path.current')->getPath(); //returns /node/1234
    $vendor_id = basename($current_path);

    $form['container'] = [
      '#type' => 'container',
      '#attributes' => [
        'class' => ['container', 'px-xl-5', 'searching']
      ]
    ];
    $form['container']['row'] = [
      '#type' => 'container',
      '#attributes' => [
        'class' => ['search-wrapper',]
      ]
    ];

    $form['container']['row']['col1'] = [
      '#type' => 'container',
      '#attributes' => [
        'class' => ['search-input']
      ]
    ];
    $form['container']['row']['col2'] = [
      '#type' => 'container',
      '#attributes' => [
        'class' => ['search-buttons']
      ]
    ];
    $form['container']['row']['col1']['searching'] = [
      '#type' => 'textfield',
      // '#title' => 'Search',
      // '#attributes'  => ['id' => 'txt_search'],
      '#placeholder' => 'Skriv här',
      '#autocomplete_route_name' => 'repair.searching.autocomplete',
      '#autocomplete_route_parameters' => ['id' => $vendor_id],
    ];

    $form['container']['row']['col2']['actions']['submit'] = [
      '#type' => 'button',
      '#title' => $this->t('Search'),
      '#value' => $this->t('Search'),
      '#ajax' => ['callback' => '::makeList'],
      // '#attributes'  => ['id' => 'btn_search', 'class' => array('btn-lg btn-primary pull-right')],
      '#button_type' => 'primary',
      '#suffix' => '<div id="clear_search" class="btn btn-success">x</div>',
    ];
    // id.addEventListener('click', () => document.getElementById('edit-searching').value='')

    $form['container']['row2'] = [
      '#type' => 'container',
      '#attributes' => [
        'class' => ['row',]
      ]
    ];

    $form['container']['row2']['col'] = [
      '#type' => 'container',
      '#attributes' => [
        'class' => ['col', 'mb-3']
      ]
    ];
    $form['container']['row2']['col']['models_list'] = [
      '#type' => 'models_list',
      '#title' => Tax::getParentName($vendor_id) . ' - ' . Tax::getName($vendor_id),
      '#list_models' => Model::loadMultiple($vendor_id),
      '#attributes' => ['class' => ['error']],
      '#error' => 'no_error',
      '#prefix'      => '<div class="form-group text-center mt-5 mb-5 placeholder make_list_result">',
      '#suffix'      => '</div>',
    ];

    $form['#cache']['max-age'] = 0;
    // Disable browser HTML5 validation
    $form['#attributes']['novalidate'] = 'novalidate';

    return $form;
  }

  public function makeList(array $form, FormStateInterface $form_state)
  {
    $current_path = \Drupal::service('path.current')->getPath(); //returns /node/1234
    $vendor_id = basename($current_path);

    $machine_name = 'models_block';
    $configuration = ['vendor_id' => $vendor_id, 'searching' => $form_state->getValue('searching')];
    $block = \Drupal::service('plugin.manager.block')->createInstance($machine_name, $configuration);
    $rendered = 'na';

    if (isset($block) && !empty($block)) {
      $render = $block->build($configuration);
      // Add the cache tags/contexts.
      \Drupal::service('renderer')->addCacheableDependency($render, $block);
      $rendered = \Drupal::service('renderer')->render($render);
    }
    // $c = $block->getContextDefinitions();
    $response = new AjaxResponse();
    // $response->addCommand(new HtmlCommand('.make_list_result', sprintf("%s", $form_state->getValue('searching'))));

    $response->addCommand(new HtmlCommand('.make_list_result', sprintf("%s", $rendered)));
    return $response;
  }

  /**
   * {@inheritdoc}
   */
  public function validateForm(array &$form, FormStateInterface $form_state)
  {
    if (empty($form_state->getValue('searching'))) {
      $form_state->setErrorByName('searching', $this->t('Please type a model'));
      // $form['service']['#error'] = $form['service']['#attributes']['class'][0];
      $form['searching']['#error'] = 'error';
    } else {
      $form['searching']['#error'] = 'no_error';
    }

    if (mb_strlen($form_state->getValue('searching')) < 3) {
      $form_state->setErrorByName('searching', $this->t('Your model name should be at least 3 characters.'));
    }
  }

  /**
   * {@inheritdoc}
   */
  public function submitForm(array &$form, FormStateInterface $form_state)
  {
    $form_state->setRedirect('<front>');
    // $this->create_node($form_state);
    $this->messenger()->addStatus($this->t('The message has been sent.'));
  }
}
