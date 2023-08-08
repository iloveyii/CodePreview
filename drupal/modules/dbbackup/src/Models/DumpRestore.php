<?php

namespace Drupal\dbbackup\Models;

use Consolidation\AnnotatedCommand\Attributes\Help;
use Drupal\dbbackup\Models\IRestore;
use Drupal\dbbackup\Models\Helper;
use mikehaertl\shellcommand\Command;

/**
 * This path is used to upload dump file to
 */
define('RESTORE_PATH', 'public://restore');


class DumpRestore implements IRestore
{
    private $host, $db_name, $db_user, $db_pass, $file_path;

    /**
     * Constructor
     * Restore for the mysql db from dump file
     */
    public function __construct($host, $db_name, $db_user, $db_pass)
    {
        $this->host = $host;
        $this->db_name = $db_name;
        $this->db_user = $db_user;
        $this->db_pass = $db_pass;
        $this->file_path = RESTORE_PATH;
    }

    /**
     * @inheritdoc
     */
    public function startRestore()
    {
        // Unzip
        $zipFile = Helper::getLatestFile(RESTORE_PATH, '/.*\.gz/');
        $sqlFile = $this->unZip($zipFile, \Drupal::service('file_system')->realpath(RESTORE_PATH));
        $sqlFilePath = sprintf("%s/%s", \Drupal::service('file_system')->realpath(RESTORE_PATH), $sqlFile);

        sleep(2);
        $command = new Command([
            'command' => '/usr/bin/mysql -h$host -u$user -p$pass $db < $path',
            'procEnv' => [
                'host' => $this->host,
                'user' => $this->db_user,
                'pass' => $this->db_pass,
                'db' => $this->db_name,
                'path' => $sqlFilePath
            ]
        ]);

        Helper::logger("Command for restore ran - {$sqlFilePath} : " . $command->getExecCommand());

        if (Helper::fileExists($sqlFilePath) &&  $command->execute()) {
            Helper::logger("Command for restore ran output - " . $command->getOutput());
            return true;
        } else {
            Helper::logger(sprintf("The restore command failed with error: %s & exit code: %s", '$command->getError()', $command->getExitCode()));
            return false;
        }
    }

    /**
     * @inheritdoc
     */
    public function isEnabled()
    {
        return `which mysql`;
    }

    /**
     * Unzip file
     */
    protected function unZip($file_path, $destination)
    {
        $command = new Command([
            'command' => 'tar -xzvf $path -C $destination',
            'procEnv' => [
                'path' => $file_path,
                'destination' => $destination
            ]
        ]);

        Helper::logger("Command for unzip ran -  file: {$file_path}, des : {$destination} " . $command->getExecCommand());

        if (Helper::fileExists(\Drupal::service('file_system')->realpath($file_path)) &&  $command->execute()) {
            return $command->getOutput();
        } else {
            Helper::logger(sprintf("The unzip command failed with error: %s & exit code: %s", $command->getError(), $command->getExitCode()));
            return false;
        }
    }
}
