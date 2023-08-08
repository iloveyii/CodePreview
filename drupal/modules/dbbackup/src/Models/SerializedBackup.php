<?php

namespace Drupal\dbbackup\Models;

use Drupal\Core\File\FileSystemInterface;
use Drupal\node\Entity\Node;
use Drupal\dbbackup\Models\IBackup;
use Drupal\dbbackup\Models\Helper;
use Drupal\dbbackup\Models\BaseBackup;
use Drupal\Core\StreamWrapper\PublicStream;


class SerializedBackup extends BaseBackup implements IBackup
{
    private $selected_types;

    /**
     * Constructor
     * 
     * Backup using docker exec command
     */
    public function __construct($selected_types)
    {
        $this->selected_types = $selected_types;
    }

    /**
     * Get class name in lowercase
     */
    public function getClassName()
    {
        $parts = explode('\\', __CLASS__);
        return strtolower(array_pop($parts));
    }

    /**
     * @inheritdoc
     */
    public function getBackup()
    {
        $serializer = \Drupal::service('serializer');
        Helper::logger("Content types recieved in SerializedBackup class:" . print_r($this->selected_types, true));
        // Clean
        \Drupal::service('file_system')->deleteRecursive($this->getBackupFilePath());

        try {
            foreach ($this->selected_types as $type) {
                $content_type_dir = sprintf("%s/%s", $this->getBackupFilePath(), $type);
                \Drupal::service('file_system')->prepareDirectory($content_type_dir, FileSystemInterface::CREATE_DIRECTORY);
                $nodes = \Drupal::entityTypeManager()->getStorage('node')->loadByProperties(['type' => $type]);

                foreach ($nodes as $node) {
                    if (($node instanceof Node) && (!is_null($node)) && (!is_null($node->id()))) {
                        $data = $serializer->serialize($node, 'json', ['plugin_id' => 'entity']);
                        $destination = sprintf("%s/%d.ser", $content_type_dir, $node->id());
                        \Drupal::service('file_system')->saveData($data, $destination, FileSystemInterface::EXISTS_REPLACE);
                    }
                }
            }
            return true;
        } catch (\Exception $e) {
            Helper::logger($e->getMessage());
            return false;
        }
    }

    /**
     * @inheritdoc
     */
    public function isEnabled()
    {
        return \Drupal::moduleHandler()->moduleExists('serialization');
    }

    /**
     * @override
     */
    public function getBackupFilePathZipped()
    {
        $source = $this->getBackupFilePath();
        $destination = sprintf("%s/backup.zip", $this->getBackupDirPath());
        \Drupal::service('ziping.service')->zipDir($source, $destination);
        return $destination;
    }

    /**
     * @override
     */
    public function getBackupDirPath()
    {
        $path = sprintf("%s/%s", \Drupal::service('file_system')->realpath(PublicStream::basePath()), $this->getClassName());

        if (Helper::fileExists($path)) {
            return $path;
        }
        return $this->createBackupDirPath($path);
    }

    /**
     * @override
     */
    public function getBackupFilePath()
    {
        $file_path = sprintf("%s/backup", $this->getBackupDirPath());
        return $file_path;
    }
}
