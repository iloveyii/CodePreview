<?php

namespace Drupal\animate\Plugin\Block;

use Drupal\Core\Block\BlockBase;


/**
 * Provides a 'Pure' Block.
 *
 * @Block(
 *   id = "pure_block",
 *   admin_label = @Translation("Pure CSS Block"),
 *   category = @Translation("Custom"),
 * )
 */
class PureBlock extends BlockBase
{
    /**
     * {@inheritdoc}
     */
    public function build()
    {
        return [
            '#type' => 'threed',
            '#text' => 'Soft\a    Hem!',
            '#link' => '/',
            '#size' => 'md',
            '#bg' => '', // bg, ''
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
