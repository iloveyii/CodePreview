<?php

namespace Drupal\dbbackup\Models;

use Consolidation\AnnotatedCommand\Attributes\Help;
use Drupal\dbbackup\Models\IRestore;
use Drupal\dbbackup\Models\Helper;
use mikehaertl\shellcommand\Command;
use Drupal\node\Entity\Node;
use Drupal\application\Model\Helper as NodeHelper;

/**
 * This path is used to upload dump file to
 */
define('SR_RESTORE_PATH', 'public://restore');


class SerializedRestore implements IRestore
{
    private $file_path;
    private $selected_types = [];

    /**
     * Constructor
     * Restore from serialized files in the directory
     */
    public function __construct($selected_types)
    {
        $this->file_path = \Drupal::service('file_system')->realpath(SR_RESTORE_PATH);
        $this->selected_types = $selected_types;
    }

    /**
     * @inheritdoc
     */
    public function startRestore()
    {
        // Unzip dir
        $serilazedDirPath = $this->getBackupFilePath();

        // Import serialized files
        $this->importSerializedFiles($serilazedDirPath);
        return true;
    }

    /**
     * Import serialzed files into content types
     * 
     * @param string $serilazedDirPath
     *  The directory containing sub-directories of content types 
     *  Each sub-category has respective ser files
     * 
     * @return boolean number of nodes imported
     */
    private function importSerializedFiles($serilazedDirPath)
    {
        Helper::logger("Serialized path is: $serilazedDirPath");
        $dirs = glob(sprintf("%s/*", $serilazedDirPath), GLOB_ONLYDIR);
        Helper::logger('Subdirs are : ' . print_r($dirs, true));
        $serializer = \Drupal::service('serializer');
        $totol_sers = 0;

        foreach ($dirs as $dir) {
            $content_type = basename($dir);
            // @Todo force import based on checkbox in settings form
            if (!in_array($content_type, $this->selected_types)) {
                Helper::logger('Content type is not selected in settings: ' . $content_type);
                continue;
            }
            $subdir_path = sprintf("%s/*", $dir);
            $sers = glob($subdir_path);
            Helper::logger(sprintf("Importing content type %s ... (%d)", $content_type, count($sers)));
            $totol_sers += count($sers);

            foreach ($sers as $ser_path) {
                $nid = basename($ser_path, '.ser');
                $fh = fopen($ser_path, 'r');
                $data = fread($fh, filesize($ser_path));
                fclose($fh);
                // First check if node exist
                $node  = Node::load($nid);
                try {
                    if ($node instanceof Node) { // update
                        // Helper::logger("{$nid} node already exists ... skipping import");
                        // continue;
                        $oldNode = Node::load($nid);
                        $oldNode = $this->updateNode($oldNode, $node);
                        $oldNode->save(false);
                    } else { // new
                        $node = $serializer->deserialize($data, \Drupal\node\Entity\Node::class, 'json');
                        $node->save(false);
                    }
                } catch (\Exception $e) {
                    Helper::logger(sprintf("Exception - path:%s, nid:%d ", $ser_path, $nid));
                }
            }
        }
        return Helper::logger("Total nodes imported: " . $totol_sers);
    }

    private function updateNode($oldNode, $newNode)
    {
        $fields = array_keys(NodeHelper::getFields($oldNode->bundle()));
        foreach ($fields as $field) {
            if ($newNode->hasField($field)) {
                $oldNode->{$field} = $newNode->{$field};
            } else {
                Helper::logger("Field not exits in serialized file with node id : " . $newNode->id());
            }
        }
        return $oldNode;
    }

    /**
     * @inheritdoc
     */
    public function isEnabled()
    {
        return \Drupal::moduleHandler()->moduleExists('serialization');
    }

    /**
     * @inheritdoc
     */
    public function getBackupDirPath()
    {
        return $this->file_path;
    }

    /**
     * @inheritdoc
     * 
     * @return string This is the unzipped directory path
     */
    public function getBackupFilePath()
    {
        // $zipFile = Helper::getLatestFile($this->getBackupDirPath(), '/.*\.zip/');
        // Helper::unZip($zipFile, $this->getBackupDirPath());
        // $serilazedDirPath = sprintf("%s/%s", $this->getBackupDirPath(), Helper::getFileName($zipFile));
        // return $serilazedDirPath;

        // They are restored to the zip file name regardless of suffix _1, _2 etc
        $serilazedDirPath = sprintf("%s/%s", $this->getBackupDirPath(), 'backup');
        return $serilazedDirPath;
    }
}
