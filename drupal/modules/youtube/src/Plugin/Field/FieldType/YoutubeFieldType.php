<?php

namespace Drupal\youtube\Plugin\Field\FieldType;

use Drupal\Core\Field\FieldItemBase;
use Drupal\Core\Field\FieldStorageDefinitionInterface;
use Drupal\Core\TypedData\DataDefinition;
use Drupal\Component\Utility\Random;
use Drupal\Core\Field\FieldDefinitionInterface;
use Drupal\Core\Form\FormStateInterface;
use Drupal\Core\StringTranslation\TranslatableMarkup;

/**
 * @FieldType(
 *      id = "youtube_field_type",
 *      label = @Translation("Youtube"),
 *      description = @Translation("Stores the youtube link"),
 *      category = @Translation("Custom"),
 *      default_formatter = "youtube_formatter_type",
 *      default_widget = "youtube_field_widget",
 * )
 */
class YoutubeFieldType extends FieldItemBase
{

    /**
     * {@inheritdoc}
     */
    public static function defaultStorageSettings()
    {
        return [
            'max_length' => 255,
            'is_ascii' => FALSE,
            'case_sensitive' => FALSE,
        ] + parent::defaultStorageSettings();
    }

    /**
     * {@inheritdoc}
     */
    public static function schema(FieldStorageDefinitionInterface $field_definition)
    {
        $schema = [
            'columns' => [
                'value' => [
                    'type' => $field_definition->getSetting('is_ascii') === TRUE ? 'varchar_ascii' : 'varchar',
                    'length' => (int) $field_definition->getSetting('max_length'),
                    'binary' => $field_definition->getSetting('case_sensitive'),
                    'default' => '',
                ],
            ],
        ];

        return $schema;
    }

    /**
     * {@inheritdoc}
     */
    public static function propertyDefinitions(FieldStorageDefinitionInterface $field_definition)
    {
        $properties['value'] = DataDefinition::create("string")->setLabel(t("Youtube Link"));
        return $properties;
    }
}
