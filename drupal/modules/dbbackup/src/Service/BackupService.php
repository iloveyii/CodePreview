<?php

/**
 * @file
 * Contains Drupal\dbbackup\Service\BackupService
 */

namespace Drupal\dbbackup\Service;

use Consolidation\AnnotatedCommand\Attributes\Help;
use Drupal\dbbackup\Models\Helper;
use Drupal\dbbackup\Models\Backup;
use Drupal\dbbackup\Models\SocatBackup;
use Drupal\dbbackup\Models\DockerBackup;
use Drupal\dbbackup\Models\LocalBackup;
use Drupal\dbbackup\Models\SerializedBackup;


class BackupService
{
    /**
     * Socat backup
     * 
     * Used between two containers. For example drupal is running on system while db on another
     * Must use socat scripts
     */
    public function getSocatBackup()
    {
        $context = new Backup(new SocatBackup(getenv('DB_SERVICE_NAME')));
        if ($context->isEnabled()) {
            if ($context->takeBackup()) {
                $context->getBackupFilePathZipped();
                return $context->getStrategy();
            }
        }
        Helper::logger("The SocatBackup service is not enabled");
        return false;
    }
    public function isEnabledSocatBackup()
    {
        $context = new Backup(new SocatBackup(getenv('DB_SERVICE_NAME')));
        return $context->isEnabled();
    }

    /**
     * Docker backup
     * 
     * Used between two systems or containers. Forexample docker on one system & db on another
     * Must have installed docker on drupal (usually local computer)
     */
    public function getDockerBackup()
    {
        $context = new Backup(new DockerBackup(getenv('DB_SERVICE_NAME'), 'localhost', getenv('MYSQL_DATABASE'), getenv('MYSQL_USER'), getenv('MYSQL_PASSWORD')));
        if ($context->isEnabled()) {
            if ($context->takeBackup()) {
                $context->getBackupFilePathZipped();
                return $context->getStrategy();
            }
        }
        Helper::logger("The DockerBackup service is not enabled");
        return false;
    }

    public function isEnabledDockerBackup()
    {
        $context = new Backup(new DockerBackup(getenv('DB_SERVICE_NAME'), 'localhost', getenv('MYSQL_DATABASE'), getenv('MYSQL_USER'), getenv('MYSQL_PASSWORD')));
        return $context->isEnabled();
    }


    /**
     * Local or remote backup using mysqldump
     * 
     * Use to backup a local or remote mysql database using mysqldump command line
     * Must have installed mysqldump on drupal system
     */
    public function getLocalBackup()
    {
        $context = new Backup(new LocalBackup(getenv('DB_SERVICE_NAME'), getenv('MYSQL_DATABASE'), getenv('MYSQL_USER'), getenv('MYSQL_PASSWORD')));
        if ($context->isEnabled()) {
            if ($context->takeBackup()) {
                $context->getBackupFilePathZipped();
                return $context->getStrategy();
            }
        }

        Helper::logger("The LocalBackup service is not enabled");
        return false;
    }

    public function isEnabledLocalBackup()
    {
        $context = new Backup(new LocalBackup(getenv('DB_SERVICE_NAME'), getenv('MYSQL_DATABASE'), getenv('MYSQL_USER'), getenv('MYSQL_PASSWORD')));
        return $context->isEnabled();
    }


    /**
     * Local or remote backup using mysqldump
     * 
     * Use to backup a local or remote mysql database using mysqldump command line
     * Must have installed mysqldump on drupal system
     */
    public function getSerializedBackup($selected_types)
    {
        $context = new Backup(new SerializedBackup($selected_types));
        if ($context->isEnabled()) {
            if ($context->takeBackup()) {
                $context->getBackupFilePathZipped();
                return $context->getStrategy();
            }
        }

        Helper::logger("The SerializedBackup service is not enabled");
        return false;
    }

    public function isEnabledSerializedBackup()
    {
        $context = new Backup(new SerializedBackup(null));
        return $context->isEnabled();
    }
}
