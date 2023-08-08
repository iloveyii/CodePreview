<?php

/**
 * @file
 * Definition of Drupal\views_carousel\Plugin\views\style\ViewsCarousel.
 */

namespace Drupal\views_carousel\Plugin\views\style;

use Drupal\core\form\FormStateInterface;
use Drupal\views\Plugin\views\style\StylePluginBase;
use Drupal\views\Plugin\views\field\FieldPluginBase;
use Drupal\views\ResultRow;
use Drupal\Core\Cache\CacheableDependencyInterface;
use Drupal\Core\Cache\Cache;

/**
 * Style plugin to render a list of years and months
 * in reverse chronological order linked to content.
 *
 * @ingroup views_style_plugins
 *
 * @ViewsStyle(
 *   id = "views_carousel",
 *   title = @Translation("Views Carousel"),
 *   help = @Translation("Render a list of image in Carousel format"),
 *   theme = "views_view_views_carousel",
 *   display_types = { "normal" }
 * )
 *
 */
class ViewsCarousel extends StylePluginBase
{

    /**
     * Does the style plugin for itself support to add fields to its output.
     *
     * @var bool
     */
    protected $usesFields = TRUE;

    /**
     * {@inheritdoc}
     */
    protected $usesRowPlugin = TRUE;

    /**
     * Does the style plugin support custom css class for the rows.
     *
     * @var bool
     */
    protected $usesRowClass = TRUE;

    /**
     * Does the style plugin support grouping of rows.
     *
     * @var bool
     */
    protected $usesGrouping = FALSE;

    /**
     * Should field labels be enabled by default.
     *
     * @var bool
     */
    protected $defaultFieldLabels = TRUE;

    /**
     * Contains the current active sort column.
     * @var string
     */
    public $active;

    /**
     * Contains the current active sort order, either desc or asc.
     * @var string
     */
    public $order;


    /**
     * Normalize a list of columns based upon the fields that are
     * available. This compares the fields stored in the style handler
     * to the list of fields actually in the view, removing fields that
     * have been removed and adding new fields in their own column.
     *
     * - Each field must be in a column.
     * - Each column must be based upon a field, and that field
     *   is somewhere in the column.
     * - Any fields not currently represented must be added.
     * - Columns must be re-ordered to match the fields.
     *
     * @param $columns
     *   An array of all fields; the key is the id of the field and the
     *   value is the id of the column the field should be in.
     * @param $fields
     *   The fields to use for the columns. If not provided, they will
     *   be requested from the current display. The running render should
     *   send the fields through, as they may be different than what the
     *   display has listed due to access control or other changes.
     *
     * @return array
     *   An array of all the sanitized columns.
     */
    public function sanitizeColumns($columns, $fields = NULL)
    {
        $sanitized = [];
        if ($fields === NULL) {
            $fields = $this->displayHandler->getOption('fields');
        }
        // Preconfigure the sanitized array so that the order is retained.
        foreach ($fields as $field => $info) {
            // Set to itself so that if it isn't touched, it gets column
            // status automatically.
            $sanitized[$field] = $field;
        }

        foreach ($columns as $field => $column) {
            // first, make sure the field still exists.
            if (!isset($sanitized[$field])) {
                continue;
            }

            // If the field is the column, mark it so, or the column
            // it's set to is a column, that's ok
            if ($field == $column || $columns[$column] == $column && !empty($sanitized[$column])) {
                $sanitized[$field] = $column;
            }
            // Since we set the field to itself initially, ignoring
            // the condition is ok; the field will get its column
            // status back.
        }

        return $sanitized;
    }


    /**
     * Set default options
     */
    protected function defineOptions()
    {
        $options = parent::defineOptions();

        $options['columns'] = ['default' => []];
        $options['default'] = ['default' => ''];
        $options['interval'] = ['default' => 7];
        $options['controls'] = ['default' => TRUE];
        $options['indicators'] = ['default' => TRUE];
        $options['captions'] = ['default' => FALSE];
        $options['animation'] = ['default' => ''];

        // Columns
        $options['title'] = ['default' => ''];
        $options['summary'] = ['default' => ''];
        $options['image_url'] = ['default' => ''];

        return $options;
    }

    /**
     * {@inheritdoc}
     */
    public function buildOptionsForm(&$form, FormStateInterface $form_state)
    {
        parent::buildOptionsForm($form, $form_state);

        // Path prefix for ViewsCarousel links.
        $form['interval'] = array(
            '#type' => 'textfield',
            '#title' => t('Sliding interval(s)'),
            '#default_value' => (isset($this->options['interval'])) ? $this->options['interval'] : 5,
        );

        // With controls
        $form['controls'] = array(
            '#type' => 'checkbox',
            '#title' => t('Show controls'),
            '#default_value' => (isset($this->options['controls'])) ? $this->options['controls'] : 0,
        );

        // With indicators
        $form['indicators'] = array(
            '#type' => 'checkbox',
            '#title' => t('Show indicators'),
            '#default_value' => (isset($this->options['indicators'])) ? $this->options['indicators'] : 0,
        );

        // With captions
        $form['captions'] = array(
            '#type' => 'checkbox',
            '#title' => t('Show captions'),
            '#default_value' => (isset($this->options['captions'])) ? $this->options['captions'] : 0,
        );

        // Animations
        $options = array(
            'slide' => 'Slide',
            'carousel-fade' => 'Crossfade',
        );
        $form['animation'] = array(
            '#type' => 'radios',
            '#title' => t('Animation'),
            '#options' => $options,
            '#default_value' => (isset($this->options['animation'])) ? $this->options['animation'] : 1,
            '#description' => t('What is the animation image sliding? <br />'),
        );

        // Extra CSS classes.
        $form['classes'] = array(
            '#type' => 'textfield',
            '#title' => t('CSS classes'),
            '#default_value' => (isset($this->options['classes'])) ? $this->options['classes'] : 'view-views_carousel',
            '#description' => t('CSS classes for further customization of this ViewsCarousel page.'),
        );

        // Make Table
        // $form['#theme'] = 'viewscarouseltable';
        // $columns = $this->sanitizeColumns($this->options['columns']);

        // // Create an array of allowed columns from the data we know:
        // $field_names = $this->displayHandler->getFieldLabels();

        // if (isset($this->options['default'])) {
        //     $default = $this->options['default'];
        //     if (!isset($columns[$default])) {
        //         $default = -1;
        //     }
        // } else {
        //     $default = -1;
        // }

        $field_names = $this->displayHandler->getFieldLabels();
        $form['title'] = [
            '#title' => $this->t('Title'),
            '#type' => 'select',
            '#options' => $field_names,
            '#default_value' => (isset($this->options['title'])) ? $this->options['title'] : '',
        ];

        $form['summary'] = [
            '#title' => $this->t('Summary'),
            '#type' => 'select',
            '#options' => $field_names,
            '#default_value' => (isset($this->options['summary'])) ? $this->options['summary'] : '',
        ];

        $form['image_url'] = [
            '#title' => $this->t('Image'),
            '#type' => 'select',
            '#options' => $field_names,
            '#default_value' => (isset($this->options['image_url'])) ? $this->options['image_url'] : '',
        ];
    }

    // Not allowd in Style or may be need Proper render array
    public function render2()
    {
        $size = '300x200';
        list($w, $h) = explode('x', $size);

        // Return a renderable element. In this case we are returning an image, but
        // a string or any other markup is also possible.
        return [
            '#theme' => 'image',
            '#uri' => sprintf("https://rimages.softhem.net/r/%d/%d?r=%d", $w, $h, rand(0, 100000000)),
            '#alt' => 'Some title',
        ];

        // $variables['table'] = [
        //     '#type' => 'table',
        //     '#theme' => 'table__views_ui_style_plugin_table',
        //     '#header' => $header,
        //     '#rows' => $rows,
        // ];
    }
}
