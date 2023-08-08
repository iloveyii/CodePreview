<?php

namespace Drupal\articles\Plugin\views\filter;

use Drupal\Core\Form\FormStateInterface;
use Drupal\node\Entity\Node;
use Drupal\views\Plugin\views\filter\FilterPluginBase;
use Drupal\views\Plugin\views\display\DisplayPluginBase;
use Drupal\views\Plugin\views\filter\InOperator;
use Drupal\views\Plugin\views\filter\LatestRevision;
use Drupal\views\ViewExecutable;
use Symfony\Component\DependencyInjection\ContainerInterface;

/**
 * Latest article filter
 * 
 * @ingroup views_filter_handlers
 * 
 * @ViewsFilter("articles_latest_article_filter")
 */
class LatestArticleFilter extends InOperator
{


    /**
     * Constructs a LatestArticleFilter plugin object
     * 
     * @param array $configuration
     *  Info about plugin instance
     * @param string $plugin_id
     *  The id of this plugin
     * @param mixed $plugin_definition
     *  The plugin implementation definition
     */
    public function __construct(array $configuration, $plugin_id, $plugin_definition)
    {
        parent::__construct($configuration, $plugin_id, $plugin_definition);
    }

    /**
     * {@inheritdoc}
     */
    public static function create(ContainerInterface $container, array $configuration, $plugin_id, $plugin_definition)
    {
        return new static($configuration, $plugin_id, $plugin_definition);
    }

    /**
     * {@inheritdoc}
     */
    public function init(ViewExecutable $view, DisplayPluginBase $display, array &$options = NULL)
    {
        parent::init($view, $display, $options);
        $this->valueTitle = $this->t('Articles of type');
        $this->definition['options callback'] = [$this, 'getLatestArticles'];
    }

    /**
     * Generates the list of Articles than can be used with the filter
     */
    public function getLatestArticles()
    {
        // Filter out latest article
        $q = \Drupal::entityQuery('node')
            ->condition('type', 'landing_page')
            ->sort('created', 'DESC')
            ->range(0, 1)
            ->accessCheck(FALSE);
        $ids = $q->execute();
        $node = Node::load(reset($ids));
        $title = 'Drupal Concepts';
        if ($node instanceof Node) {
            $title = $node->getTitle();
        }
        $result[$title] = 'Latest promoted article';
        return $result;
    }

    // protected function defineOptions()
    // {
    //     $options = parent::defineOptions();

    //     $options['relative_date'] = ['default' => ''];

    //     return $options;
    // }

    /**
     * {@inheritdoc}
     */
    // public function init(ViewExecutable $view, DisplayPluginBase $display, array &$options = NULL)
    // {
    //     parent::init($view, $display, $options);
    //     $this->valueTitle = t('Allowed node titles');
    //     $this->definition['options callback'] = array($this, 'generateOptions');
    // }

    // public function generateOptions()
    // {
    //     return 'My value';
    // }

    /**
     * Form with all possible filter values.
     */
    // protected function valueForm(&$form, FormStateInterface $form_state)
    // {
    //     $form['value'] = [
    //         '#tree' => TRUE,
    //         'relative_date' => [
    //             '#type' => 'select',
    //             '#title' => $this->t('Subscribed'),
    //             '#options' => [
    //                 'all' => $this->t('All'),
    //                 'last_week' => $this->t('Last Week'),
    //                 'last_month' => $this->t('Last Month'),
    //             ],
    //             '#default_value' => !empty($this->value['relative_date']) ? $this->value['relative_date'] : 'all',
    //         ]
    //     ];
    // }

    /**
     * Adds conditions to the query based on the selected filter option.
     */
    // public function query()
    // {
    //     $this->ensureMyTable();
    //     $date = "$this->tableAlias.$this->realField";
    //     switch ($this->value['relative_date']) {
    //         case 'last_week':
    //             $last_week_time = strtotime("first day of previous week");
    //             $this_week_time = strtotime("first day of this week");
    //             $last_week = "FROM_UNIXTIME(" . $last_week_time . ")";
    //             $this_week = "FROM_UNIXTIME(" . $this_week_time . ")";
    //             $this->query->addWhereExpression($this->options['group'], "$date >= $last_week AND $date < $this_week");
    //             break;
    //         case 'last_month':
    //             $last_month = strtotime("first day of previous month");
    //             $this_month = strtotime("first day of this month");
    //             $this->query->addWhereExpression($this->options['group'], "$date >= $last_month AND $date < $this_month");
    //             break;
    //     }
    // }

    // public function adminSummary()
    // {
    //     if ($this->isAGroup()) {
    //         return $this->t('grouped');
    //     }
    //     $dt = !empty($this->value['relative_date']) ? $this->value['relative_date'] : 'all';
    //     if (!empty($this->options['exposed'])) {
    //         return $this->t('exposed') . ', ' . $this->t('default state') . ': ' . $dt;
    //     } else {
    //         return $dt . ': ' . $dt;
    //     }
    // }
}


// Uncaught PHP Exception InvalidArgumentException: "The configuration property 
// display.default.display_options.filters.articles.value.relative_date doesn't exist.
// " at /opt/web/core/lib/Drupal/Core/Config/Schema/ArrayElement.php line 76, referer: http://localhost/admin/structure/views/view/try
// @see d8views for some suggestions