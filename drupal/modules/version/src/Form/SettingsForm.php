<?php

/**
 * @file
 * Contains \Drupal\version\Form\SettingsForm
 */

namespace Drupal\version\Form;

use Drupal\Core\Ajax\AjaxResponse;
use Drupal\Core\Ajax\HtmlCommand;
use Drupal\Core\Form\ConfigFormBase;
use Drupal\Core\Form\FormStateInterface;
use Symfony\Component\HttpFoundation\Request;
use Drupal\version\Models\Helper;
use Drupal\Component\Utility\Environment;


class SettingsForm extends ConfigFormBase
{
    /**
     * {@inheritdoc}
     */
    public function getFormID()
    {
        return 'version_admin_settings';
    }

    /**
     * {@inheritdoc}
     */
    public function getEditableConfigNames()
    {
        return ['version.settings'];
    }

    /**
     * {@inheritdoc}
     */
    public function buildForm(array $form, FormStateInterface $form_state, Request $request = null)
    {
        $form = [
            '#attributes' => ['enctype' => 'multipart/form-data']
        ];

        /**
         * Download db
         */
        // Tabs
        $form['htabs'] = [
            '#type' => 'horizontal_tabs',
            // '#tree' => TRUE,
            '#prefix' => '<div id="version" class="p-3">',
            '#suffix' => '</div>',
        ];

        // Tab 1
        $form['htabs']['tab1'] =  $this->tab1();

        // Tab 2
        $form['htabs']['tab2'] =  $this->tab2();

        // Tab 3

        $form['#attached']['library'][] = 'field_group/formatter.horizontal_tabs';

        // Dont cache
        $form_state->disableCache();

        // Don't cache this page
        \Drupal::service('page_cache_kill_switch')->trigger();
        $form['#cache'] = ['max-age' => 0];

        return parent::buildForm($form, $form_state);
    }

    // Tab 1
    private function tab1()
    {
        $form = [
            '#type' => 'details',
            '#title' => 'Configurations',
            '#collapsible' => TRUE,
            '#collapsed' => TRUE,
        ];


        // Make a panel
        $form['panel'] = [
            '#type' => 'container',
            '#attributes' => ['class' => ['panel']],
            'inner' => [
                'h3' => [
                    '#markup' => '<h3 class="panel__title">Font & Module</h3>',
                ],
                'content' => [
                    '#type' => 'container',
                    '#attributes' => ['class' => ['panel__content']],
                    'ul' => [
                        '#type' => 'html_tag',
                        '#tag' => 'ul',
                        '#attributes' =>  ['class' => ['admin-list']],
                        'font_sizes' => [
                            '#type' => 'html_tag',
                            '#tag' => 'li',
                            'a' => [
                                '#type' => 'html_tag',
                                '#tag' => 'a',
                                '#attributes' => ['href' => ['#']],
                                'span' => [
                                    '#markup' => '<span class="label">Font size</span>',
                                ],
                                'container' => [
                                    '#type' => 'container',
                                    '#attributes' => ['class' => ['description']],
                                    'font_size' => [
                                        '#type' => 'radios',
                                        '#title' => '',
                                        '#options' => $this->config('version.settings')->get('font_size'), // ['xs' => 'Extra Small', 'sm' => 'Small', 'md' => 'Medium', 'lg' => 'Large'],
                                        '#default_value' => $this->config('version.settings')->get('selected_font_size')
                                    ],
                                ]
                            ]
                        ],
                        'modules' => [
                            '#type' => 'html_tag',
                            '#tag' => 'li',
                            'a' => [
                                '#type' => 'html_tag',
                                '#tag' => 'a',
                                '#attributes' => ['href' => ['#']],
                                'span' => [
                                    '#markup' => '<span class="label">Modules</span>',
                                ],
                                'container' => [
                                    '#type' => 'container',
                                    '#attributes' => ['class' => ['description']],
                                    'modules' => [
                                        '#type' => 'radios',
                                        '#title' => '',
                                        '#options' => \Drupal::service('versions.service')->modules(),
                                        '#default_value' => $this->config('version.settings')->get('selected_module')
                                    ]
                                ]
                            ]
                        ],
                        'db_version' => [
                            '#type' => 'html_tag',
                            '#tag' => 'li',
                            'a' => [
                                '#type' => 'html_tag',
                                '#tag' => 'a',
                                '#attributes' => ['href' => ['#']],
                                'span' => [
                                    '#markup' => '<span class="label">Database Version</span>',
                                ],
                                'container' => [
                                    '#type' => 'container',
                                    '#attributes' => ['class' => ['description md-4 xs-12 sm-6']],
                                    'db_version' => [
                                        '#title' => '',
                                        '#type' => 'textfield',
                                        '#size' => 32,
                                        '#placeholder' => t('Current db version'),
                                        '#required' => false,
                                        '#default_value' => \Drupal::service('versions.service')->current($this->config('version.settings')->get('selected_module')),
                                        '#maxlength' => 64,
                                    ]
                                ]
                            ]
                        ],
                    ]
                ]
            ]
        ];

        $form['panel']['#attached']['library'][] = 'version/panel';

        return $form;
    }

    // Tab 2
    private function tab2()
    {
        $form = [
            '#type' => 'details',
            '#title' => 'Versions',
            '#collapsible' => TRUE,
            '#collapsed' => TRUE,
        ];


        // Make a panel
        $form['panel'] = [
            '#type' => 'container',
            '#attributes' => ['class' => ['panel']],
            'inner' => [
                'h3' => [
                    '#markup' => '<h3 class="panel__title">Current</h3>',
                ],
                'content' => [
                    '#type' => 'container',
                    '#attributes' => ['class' => ['panel__content']],
                    'ul' => [
                        '#type' => 'html_tag',
                        '#tag' => 'ul',
                        '#attributes' =>  ['class' => ['admin-list']],
                        'database' => [
                            '#type' => 'html_tag',
                            '#tag' => 'li',
                            'a' => [
                                '#type' => 'html_tag',
                                '#tag' => 'a',
                                '#attributes' => ['href' => ['#']],
                                'span' => [
                                    '#markup' => '<span class="label">Database</span>',
                                ],
                                'container' => [
                                    '#type' => 'container',
                                    '#attributes' => ['class' => ['description']],
                                    'db' => [
                                        '#type' => 'html_tag',
                                        '#tag' => 'div',
                                        'span' => [
                                            '#markup' => sprintf("<span class='label'>Version: %s</span>", \Drupal::service('versions.service')->current($this->config('version.settings')->get('selected_module'))),
                                        ]
                                    ],
                                ]
                            ]
                        ],
                        'environment' => [
                            '#type' => 'html_tag',
                            '#tag' => 'li',
                            'a' => [
                                '#type' => 'html_tag',
                                '#tag' => 'a',
                                '#attributes' => ['href' => ['#']],
                                'span' => [
                                    '#markup' => '<span class="label">Environment</span>',
                                ],
                                'container' => [
                                    '#type' => 'container',
                                    '#attributes' => ['class' => ['description']],
                                    'env' => [
                                        '#type' => 'html_tag',
                                        '#tag' => 'div',
                                        'span' => [
                                            '#markup' => sprintf("<span class='label'>Version: <span id='env_version'> %s </span> </span>", \Drupal::service('versions.service')->currentEnv()),
                                        ],
                                        'copy' => [
                                            '#type' => 'button',
                                            '#value' => $this->t('Copy'),
                                            '#attributes' => [
                                                'onclick' => 'return false;',
                                                'class' => ['btn btn-primary button button--primary', 'mt-3', 'px-5'],
                                                'id' => 'copy-button'
                                            ],
                                            '#attached' => [
                                                'library' => [
                                                    'version/style',
                                                ],
                                            ],
                                        ]
                                    ]
                                ]
                            ]
                        ],
                    ]
                ]
            ]
        ];

        $form['panel']['#attached']['library'][] = 'version/panel';

        return $form;
    }
    /**
     * {@inheritdoc}
     */
    public function submitForm(array &$form, FormStateInterface $form_state)
    {
        $this->config('version.settings')
            ->set('selected_font_size', $form_state->getValue('font_size'))
            ->set('selected_module', $form_state->getValue('modules'))
            ->save();
        parent::submitForm($form, $form_state);
        \Drupal::service('versions.service')->setVersion($this->config('version.settings')->get('selected_module'), $form_state->getValue('db_version'));
    }
}
