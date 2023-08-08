<?php

namespace Drupal\dbbackup\Models;

use Drupal\Core\File\FileSystemInterface;
use Drupal\Core\StreamWrapper\PublicStream;


abstract class BaseBackup
{

    public abstract function getClassName();

    /**
     * Directory of backup where we store sql file
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
     * Get the path for backup file (sql file)
     */
    public function getBackupFilePath()
    {
        $file_path = sprintf("%s/backup.sql", $this->getBackupDirPath());
        return $file_path;
    }

    /**
     * Create backup directory if not exists 
     */
    public function createBackupDirPath($path)
    {
        if (Helper::fileExists($path)) {
            return $path;
        }
        \Drupal::service('file_system')->prepareDirectory($path, FileSystemInterface::CREATE_DIRECTORY);
        return $path;
    }

    /**
     * Get Zipped file of the sql file
     */
    public function getBackupFilePathZipped()
    {
        return Helper::getBackupFilePathZipped($this->getBackupFilePath());
    }

    /**
     * Get Zipped URL for download on webpage
     */
    public function getBackupFilePathZippedUrl()
    {
        $url = sprintf("/%s/%s/%s", PublicStream::basePath(), $this->getClassName(), basename($this->getBackupFilePathZipped()));
        return $url;
    }
}
