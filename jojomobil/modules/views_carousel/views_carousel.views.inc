<?php

/**
 * @file
 * Contains custom views support and definitions.
 */

/**
 * Implements hook_views_data_alter().
 */
function views_carousel_views_data_alter(array &$data)
{

    // Field Plugin for Demo
    $data['node_field_data']['square_field_plugin'] = [
        'title' => t('Square Field Plugin'),
        'field' => [
            'help' => t('A Square field plugin.'),
            'id' => 'square_field_plugin',
        ],
    ];
}
