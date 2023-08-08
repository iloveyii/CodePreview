<?php

namespace Drupal\youtube\Plugin\Field\FieldFormatter;

use Drupal\Component\Utility\Html;
use Drupal\Core\Field\FieldItemInterface;
use Drupal\Core\Field\FieldItemListInterface;
use Drupal\Core\Field\FormatterBase;
use Drupal\Core\Form\FormStateInterface;
use Drupal\Core\Template\Attribute;

/**
 * Plugin implementation of the 'youtube_formatter_type' formatter.
 *
 * @FieldFormatter(
 *   id = "youtube_formatter_type",
 *   label = @Translation("Youtube field formatter type"),
 *   field_types = {
 *     "youtube_field_type"
 *   }
 * )
 */
class YoutubeFieldFormatter extends FormatterBase
{

    /**
     * {@inheritdoc}
     */
    public static function defaultSettings()
    {
        return [
            // Implement default settings.
        ] + parent::defaultSettings();
    }

    /**
     * {@inheritdoc}
     */
    public function settingsForm(array $form, FormStateInterface $form_state)
    {
        return [
            // Implement settings form.
        ] + parent::settingsForm($form, $form_state);
    }

    /**
     * {@inheritdoc}
     */
    public function settingsSummary()
    {
        $summary = [];
        // Implement settings summary.

        return $summary;
    }

    /**
     * {@inheritdoc}
     */
    public function viewElements(FieldItemListInterface $items, $langcode)
    {
        foreach ($items as $delta => $item) {
            $options['src'] = sprintf("https://www.youtube.com/embed/%s", $this->getYoutubeUrlCode($item));
        }


        $attributes = new Attribute($options);

        $element = [
            '#theme' => 'iframe',
            '#src' => $options['src'],
            '#attributes' => $attributes,
            '#text' => 'Youtube video',
        ];

        return $element;
    }

    /**
     * Returns the code (11 chars) part from a youtube url
     */
    protected function getYoutubeUrlCode($item)
    {
        preg_match('/v=([a-z0-9]{11})/i', $this->viewValue($item), $matches);
        if (is_array($matches) && count($matches) > 1) {
            return $matches[1];
        }
        return null;
    }




    /**
     * Generate the output appropriate for one field item.
     *
     * @param \Drupal\Core\Field\FieldItemInterface $item
     *   One field item.
     *
     * @return string
     *   The textual output generated.
     */
    protected function viewValue(FieldItemInterface $item)
    {
        // The text value has no text format assigned to it, so the user input
        // should equal the output, including newlines.
        return nl2br(Html::escape($item->value));
    }
}
