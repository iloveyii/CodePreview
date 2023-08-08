<?php

/**
 * @file
 * Contains \Drupal\dbbackup\Form\SettingsForm
 */

namespace Drupal\dbbackup\Form;

use Drupal\Core\Ajax\AjaxResponse;
use Drupal\Core\Ajax\HtmlCommand;
use Drupal\Core\Form\ConfigFormBase;
use Drupal\Core\Form\FormStateInterface;
use Symfony\Component\HttpFoundation\Request;
use Drupal\dbbackup\Models\Helper;
use Drupal\Component\Utility\Environment;


class SettingsForm extends ConfigFormBase
{
    /**
     * {@inheritdoc}
     */
    public function getFormID()
    {
        return 'dbbackup_admin_settings';
    }

    /**
     * {@inheritdoc}
     */
    public function getEditableConfigNames()
    {
        return ['dbbackup.settings'];
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
            '#prefix' => '<div id="dbbackup" class="p-3">',
            '#suffix' => '</div>',
        ];

        // Tab 1
        $form['htabs']['tab1'] =  $this->tab1();

        // Tab 2
        $form['htabs']['tab2'] =  $this->tab2();

        // Tab 3
        $form['htabs']['tab3'] =  $this->tab3();

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
        $service = \Drupal::service('service.backup');

        $form = [
            '#type' => 'details',
            '#title' => 'Backup Database',
            '#collapsible' => TRUE,
            '#collapsed' => TRUE,
        ];

        // Docker
        if ($service->isEnabledDockerBackup()) {
            $docker_actions['actions']['button'] = array(
                '#prefix' => '<p>Here you can download using Docker exec command line</p>',
                '#type' => 'button',
                '#value' => 'Download using Docker',
                '#attributes' => ['class' => ['m-0']],
                '#ajax' => ['callback' => '::dockerDownload'],
                '#suffix' => '<div class="placeholder docker_result"></div>',
            );
        } else {
            $docker_actions['actions']['info'] = array(
                '#markup' => '<p class="text-danger">Docker exec is not installed on this server, please contact developer.</p>'
            );
        }

        // Socat
        if ($service->isEnabledSocatBackup()) {
            $socat_actions['actions']['button'] = array(
                '#prefix' => '<p>Here you can download using Socat script</p>',
                '#type' => 'button',
                '#value' => 'Download using Socat',
                '#attributes' => ['class' => ['m-0']],
                '#ajax' => ['callback' => '::socatDownload'],
                '#suffix' => '<div class="placeholder socat_result"></div>',
            );
        } else {
            $socat_actions['actions']['info'] = array(
                '#markup' => '<p class="text-danger">Socat command is not installed on this server, please contact developer.</p>',
            );
        }

        // Dump
        if ($service->isEnabledLocalBackup()) {
            $dump_actions['actions']['button'] = array(
                '#prefix' => '<p>Here you can download using MySQL Dump</p>',
                '#type' => 'button',
                '#value' => 'Download using MySQL Dump',
                '#attributes' => ['class' => ['m-0']],
                '#ajax' => ['callback' => '::dumpDownload'],
                '#suffix' => '<div class="placeholder dump_result"></div>',
            );
        } else {
            $dump_actions['actions']['info'] = array(
                '#markup' => '<p class="text-danger">MYSQL is not installed on this server, please contact developer.</p>',
            );
        }

        // Serialized
        if ($service->isEnabledSerializedBackup()) {
            $serialized_actions['actions']['button'] = array(
                '#prefix' => '<p>Here you can download a zip of serialized files, you may select the content types in settings to dowload only required content types.</p>',
                '#type' => 'button',
                '#value' => 'Download using Serialization',
                '#attributes' => ['class' => ['m-0']],
                '#ajax' => ['callback' => '::serializedDownload'],
                '#suffix' => '<div class="placeholder serialized_result"></div>',
            );
        } else {
            $serialized_actions['actions']['info'] = array(
                '#markup' => '<p class="text-danger">MYSQL is not installed on this server, please contact developer.</p>',
            );
        }


        // Make a panel
        $form['panel'] = [
            '#type' => 'container',
            '#attributes' => ['class' => ['panel']],
            'inner' => [
                'h3' => [
                    '#markup' => '<h3 class="panel__title">Select a method</h3>',
                ],
                'content' => [
                    '#type' => 'container',
                    '#attributes' => ['class' => ['panel__content']],
                    'ul' => [
                        '#type' => 'html_tag',
                        '#tag' => 'ul',
                        '#attributes' =>  ['class' => ['admin-list']],
                        'docker' => [
                            '#type' => 'html_tag',
                            '#tag' => 'li',
                            'a' => [
                                '#type' => 'html_tag',
                                '#tag' => 'a',
                                '#attributes' => ['href' => ['#']],
                                'span' => [
                                    '#markup' => '<span class="label">1. Download Using Docker</span>',
                                ],
                                'container' => [
                                    '#type' => 'container',
                                    '#attributes' => ['class' => ['description']],
                                    'actions' => $docker_actions
                                ]
                            ]
                        ],
                        'socat' => [
                            '#type' => 'html_tag',
                            '#tag' => 'li',
                            'a' => [
                                '#type' => 'html_tag',
                                '#tag' => 'a',
                                '#attributes' => ['href' => ['#']],
                                'span' => [
                                    '#markup' => '<span class="label">2. Download Using Socat</span>',
                                ],
                                'container' => [
                                    '#type' => 'container',
                                    '#attributes' => ['class' => ['description']],
                                    'actions' => $socat_actions
                                ]
                            ]
                        ],
                        'dump' => [
                            '#type' => 'html_tag',
                            '#tag' => 'li',
                            'a' => [
                                '#type' => 'html_tag',
                                '#tag' => 'a',
                                '#attributes' => ['href' => ['#']],
                                'span' => [
                                    '#markup' => '<span class="label">3. Download Using Dump</span>',
                                ],
                                'container' => [
                                    '#type' => 'container',
                                    '#attributes' => ['class' => ['description']],
                                    'actions' => $dump_actions
                                ]
                            ]
                        ],
                        'serialized' => [
                            '#type' => 'html_tag',
                            '#tag' => 'li',
                            'a' => [
                                '#type' => 'html_tag',
                                '#tag' => 'a',
                                '#attributes' => ['href' => ['#']],
                                'span' => [
                                    '#markup' => '<span class="label">4. Download Using Serialization</span>',
                                ],
                                'container' => [
                                    '#type' => 'container',
                                    '#attributes' => ['class' => ['description']],
                                    'actions' => $serialized_actions
                                ]
                            ]
                        ]

                    ]
                ]
            ]
        ];

        $form['panel']['#attached']['library'][] = 'dbbackup/panel';

        return $form;
    }


    public function socatDownload(array $form, FormStateInterface $form_state)
    {
        $service = \Drupal::service('service.backup');
        $strategy = $service->getSocatBackup();
        $url = $strategy == false ? '#' : $strategy->getBackupFilePathZippedUrl();

        $response = new AjaxResponse();
        $response->addCommand(new HtmlCommand('.socat_result', sprintf("<a class='button button--primary' href='%s' download>%s</a>", $url, 'Download')));
        return $response;
    }

    public function dockerDownload(array $form, FormStateInterface $form_state)
    {
        $service = \Drupal::service('service.backup');
        $strategy = $service->getDockerBackup();
        $url = $strategy == false ? '#' : $strategy->getBackupFilePathZippedUrl();

        $response = new AjaxResponse();
        $response->addCommand(new HtmlCommand('.docker_result', sprintf("<a class='button button--primary' href='%s' download>%s</a>", $url, 'Download')));
        return $response;
    }

    public function dumpDownload(array $form, FormStateInterface $form_state)
    {
        $service = \Drupal::service('service.backup');
        $strategy = $service->getLocalBackup();
        $url = $strategy == false ? '#' : $strategy->getBackupFilePathZippedUrl();

        $response = new AjaxResponse();
        $response->addCommand(new HtmlCommand('.dump_result', sprintf("<a class='button button--primary' href='%s' download>%s</a>", $url, 'Download')));
        return $response;
    }

    public function serializedDownload(array $form, FormStateInterface $form_state)
    {
        $service = \Drupal::service('service.backup');
        $strategy = $service->getSerializedBackup($this->config('dbbackup.settings')->get('allowed_types'));
        $url = $strategy == false ? '#' : $strategy->getBackupFilePathZippedUrl();

        $response = new AjaxResponse();
        $response->addCommand(new HtmlCommand('.serialized_result', sprintf("<a class='button button--primary' href='%s' download>%s</a>", $url, 'Download')));
        return $response;
    }

    // Tab 2
    private function tab2()
    {
        $service = \Drupal::service('service.restore');
        $form = [
            '#type' => 'details',
            '#title' => 'Restore Database',
            '#collapsible' => TRUE,
            '#collapsed' => TRUE,
        ];
        /**
         * Restore db
         */
        $fieldset = [
            '#type' => 'fieldset',
            '#title' => 'Upload a file'
        ];
        $fieldset['db_zip'] = [
            '#type' => 'managed_file',
            '#multiple' => FALSE,
            '#title' => '',
            '#size' => 50,
            '#description' => t('Gzip, Zip format only'),
            '#upload_validators' => [
                'file_validate_extensions' => ['gz zip 7z 7zip'],
                'file_validate_size' => Environment::getUploadMaxSize()
            ],
            '#upload_location' => 'public://restore'
        ];

        // Dump
        if ($service->isEnabledRestoreDump()) {
            $action_dump['actions']['dump'] = array(
                '#prefix' => '<p>Click the following button to start restore</p>',
                '#type' => 'button',
                '#value' => 'Restore Dump',
                '#attributes' => ['class' => ['m-0']],
                '#ajax' => ['callback' => '::restoreDump'],
                '#suffix' => '<div class="placeholder restore_dump_result"></div>',
            );
        } else {
            $action_dump['actions']['dump'] = array(
                '#markup' => '<p class="text-danger">MYSQL is not installed on this server, please contact developer.</p>',
            );
        }


        if ($service->isEnabledRestoreSerialized()) {
            $action_serialized['actions']['serialized'] = array(
                '#prefix' => '<p>Click the following button to start restore, you may select the content types in settings to import only required content types.</p>',
                '#type' => 'button',
                '#value' => 'Restore Serialized',
                '#attributes' => ['class' => ['m-0']],
                '#ajax' => ['callback' => '::restoreSerialized'],
                '#suffix' => '<div class="placeholder restore_serialized_result"></div>',
            );
        } else {
            $action_serialized['actions']['serialized'] = array(
                '#markup' => '<p class="text-danger">Serialization is not installed on this server, please contact developer.</p>',
            );
        }

        $form['panel'] = [
            '#type' => 'container',
            '#attributes' => ['class' => ['panel']],
            'inner' => [
                'h3' => [
                    '#markup' => '<h3 class="panel__title">Select a method</h3>',
                ],
                'content' => [
                    '#type' => 'container',
                    '#attributes' => ['class' => ['panel__content']],
                    'fieldset' => $fieldset,
                    'ul' => [
                        '#type' => 'html_tag',
                        '#tag' => 'ul',
                        '#attributes' =>  ['class' => ['admin-list']],
                        'dump' => [
                            '#type' => 'html_tag',
                            '#tag' => 'li',
                            'a' => [
                                '#type' => 'html_tag',
                                '#tag' => 'a',
                                '#attributes' => ['href' => ['#']],
                                'span' => [
                                    '#markup' => '<span class="label">1. Restore using SQL dump file</span>',
                                ],
                                'container' => [
                                    '#type' => 'container',
                                    '#attributes' => ['class' => ['description']],
                                    'actions' => $action_dump
                                ]
                            ]
                        ],
                        'serialized' => [
                            '#type' => 'html_tag',
                            '#tag' => 'li',
                            'a' => [
                                '#type' => 'html_tag',
                                '#tag' => 'a',
                                '#attributes' => ['href' => ['#']],
                                'span' => [
                                    '#markup' => '<span class="label">2. Restore using serialized files</span>',
                                ],
                                'container' => [
                                    '#type' => 'container',
                                    '#attributes' => ['class' => ['description']],
                                    'actions' => $action_serialized
                                ]
                            ]
                        ],
                    ]
                ]
            ]
        ];

        return $form;
    }

    public function restoreDump(array $form, FormStateInterface $form_state)
    {
        $service = \Drupal::service('service.restore');
        $strategy = $service->restoreDump();
        $result = $strategy == false ? 'Failed' : 'Success';

        $response = new AjaxResponse();
        $response->addCommand(new HtmlCommand('.restore_dump_result', sprintf("<span class='button button--primary' download>%s</span>", $result)));
        return $response;
    }

    public function restoreSerialized(array $form, FormStateInterface $form_state)
    {
        $service = \Drupal::service('service.restore');
        $strategy = $service->restoreSerialized($this->config('dbbackup.settings')->get('allowed_types'));
        $result = $strategy == false ? 'Failed' : 'Success';

        $response = new AjaxResponse();
        $response->addCommand(new HtmlCommand('.restore_serialized_result', sprintf("<span class='button button--primary' download>%s</span>", $result)));
        return $response;
    }

    public function showInfo($info)
    {
        foreach ($info as $key => $value) {
            if (is_array($value)) {
                $msg = sprintf("%s : ", $key);
                $this->messenger()->addStatus($msg);
                $this->showInfo($value);
            } else {
                $msg = sprintf("%s -> %s", $key, $value);
                $this->messenger()->addStatus($msg);
            }
        }
    }

    // Tab 3
    private function tab3()
    {
        $types = Helper::node_type_get_names();
        $config = $this->config('dbbackup.settings');
        $form = [
            '#type' => 'details',
            '#title' => 'Settings',
            '#collapsible' => TRUE,
            '#collapsed' => TRUE,
        ];
        $settings['allowed_types'] = [
            '#type' => 'checkboxes',
            '#title' => $this->t('The content types to enable the dbbackup collection for'),
            '#default_value' => $config->get('allowed_types'),
            '#options' => $types,
            '#description' => $this->t('Add a check box to the node type which will enable dbbackup for that node')
        ];
        $settings['array_filter'] = ['#type' => 'value', '#value' => true]; // ???

        $settings['sleep_time'] = [
            '#title' => $this->t('Waiting time for backup(s)'),
            '#type' => 'textfield',
            '#size' => 15,
            '#placeholder' => $this->t('Time is seconds'),
            '#required' => false,
            '#default_value' => $config->get('sleep_time'),
        ];

        $actions['actions']['clear'] = array(
            '#prefix' => '<p>Click the following button to clear Dblog</p>',
            '#type' => 'button',
            '#value' => 'Clear Dblog',
            '#attributes' => ['class' => ['m-0']],
            '#ajax' => ['callback' => '::clearDblog'],
            '#suffix' => '<div class="placeholder clear_dblog_result"></div>',
        );


        $form['panel'] = [
            '#type' => 'container',
            '#attributes' => ['class' => ['panel']],
            'inner' => [
                'h3' => [
                    '#markup' => '<h3 class="panel__title">Serialization Content types</h3>',
                ],
                'content' => [
                    '#type' => 'container',
                    '#attributes' => ['class' => ['panel__content']],
                    'ul' => [
                        '#type' => 'html_tag',
                        '#tag' => 'ul',
                        '#attributes' =>  ['class' => ['admin-list']],
                        'settings' => [
                            '#type' => 'html_tag',
                            '#tag' => 'li',
                            'a' => [
                                '#type' => 'html_tag',
                                '#tag' => 'a',
                                '#attributes' => ['href' => ['#']],
                                'span' => [
                                    '#markup' => '<span class="label">1. Content Types</span>',
                                ],
                                'container' => [
                                    '#type' => 'container',
                                    '#attributes' => ['class' => ['description']],
                                    'actions' => $settings
                                ]
                            ]
                        ],
                        'actions' => [
                            '#type' => 'html_tag',
                            '#tag' => 'li',
                            'a' => [
                                '#type' => 'html_tag',
                                '#tag' => 'a',
                                '#attributes' => ['href' => ['#']],
                                'span' => [
                                    '#markup' => '<span class="label">2. DbLog</span>',
                                ],
                                'container' => [
                                    '#type' => 'container',
                                    '#attributes' => ['class' => ['description']],
                                    'actions' => $actions
                                ]
                            ]
                        ],
                    ]
                ]
            ]
        ];


        return $form;
    }

    public function clearDblog()
    {
        $result = Helper::clearDblog();
        sleep(2);
        $response = new AjaxResponse();
        $response->addCommand(new HtmlCommand('.clear_dblog_result', sprintf("<span class='button button--primary' download>%s</span>", 'Success')));
        Helper::logger("Run clear dblog command with result: " . $result);
        return $response;
    }
    /**
     * {@inheritdoc}
     */
    public function submitForm(array &$form, FormStateInterface $form_state)
    {
        $allowed_types = array_filter($form_state->getValue('allowed_types'));
        $sleep_time = $form_state->getValue('sleep_time');
        sort($allowed_types);
        $this->config('dbbackup.settings')
            ->set('allowed_types', $allowed_types)
            ->set('sleep_time', $sleep_time)
            ->save();
        parent::submitForm($form, $form_state);
    }
}
