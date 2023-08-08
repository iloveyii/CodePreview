<?php

namespace Drupal\views_carousel\Plugin\views\field;

use Drupal\views\Plugin\views\field\FieldPluginBase;
use Drupal\views\ResultRow;


/**
 * A blank plugin used as a starting place for making as square.
 *
 * @ViewsField("square_field_plugin")
 */
class SquareFieldPlugin extends FieldPluginBase
{
    public function query()
    {
    }

    public function render(ResultRow $row)
    {
        $entity = $row->_entity;
        $field = 'body';

        // Must have a field
        if (!$entity->hasField($field) || $entity->get($field)->isEmpty()) {
            return 'NULL';
        }

        // Get field value
        $value = str_replace([', basic_html', ', full_html'], '',   $entity->get($field)->getString());
        $value = str_replace(['page'], '<strong>PAGE</strong>',   $value);
        $content['body'] = [
            '#markup' => $value,

        ];

        return $content;

        // $output = preg_match_all('/<img.+src=[\'"]([^\'"]+)[\'"].*>/i', $node->body, $matches);
        // $node_field[0]['value'] = $matches[1][0];
    }
}
