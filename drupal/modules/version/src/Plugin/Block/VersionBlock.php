<?php

namespace Drupal\version\Plugin\Block;

use Drupal\Core\Block\BlockBase;


/**
 * Provides a 'Version' Block.
 *
 * @Block(
 *   id = "version_block",
 *   admin_label = @Translation("Version Block"),
 *   category = @Translation("Custom"),
 * )
 */
class VersionBlock extends BlockBase
{
    /**
     * {@inheritdoc}
     */
    public function build()
    {
        // Don't cache this page
        \Drupal::service('page_cache_kill_switch')->trigger();

        return [
            '#theme' => 'version',
            '#version' => \Drupal::service('versions.service')->current('site_version'),
            '#font_size' => \Drupal::config('version.settings')->get('selected_font_size'),
            '#attached' => [
                'library' => [
                    'version/style'
                ]
            ],
            '#cache' =>  ['max-age' => 0]

        ];
    }

    /**
     * Don't cache this page
     */
    public function getCacheMaxAge()
    {
        return 0;
    }
}
