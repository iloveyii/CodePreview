<?php

use Drupal\views\Plugin\views\query\QueryPluginBase;
use Drupal\views\ViewExecutable;

/**
 * Implements hook_views_data_alter().
 */
function articles_views_data_alter(array &$data)
{
    $data['node_field_data']['latest_article_filter'] = array(
        'title' => t('Articles filter'),
        'group' => t('Content'),
        'filter' => array(
            'title' => t('Articles filter'),
            'help' => t('Filter out the latest article'),
            'field' => 'title',
            'id' => 'articles_latest_article_filter'
        ),
    );
}
