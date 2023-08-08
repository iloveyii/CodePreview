<?php

namespace  Drupal\dbbackup\Models;

use  Drupal\dbbackup\Models\IBackup;
use  Drupal\dbbackup\Models\Helper;
use  Drupal\dbbackup\Models\File;
use  Drupal\dbbackup\Models\BaseBackup;
use mikehaertl\shellcommand\Command;


class SocatBackup extends BaseBackup implements IBackup
{
    private $container_name;

    /**
     * Constructor
     * 
     * Backup using socat command
     */
    public function __construct($container_name)
    {
        $this->container_name = $container_name;
        $this->getBackupDirPath();
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
        $this->ping('db_backup');
        return File::setFilePath($this->getBackupFilePath())->fileExists()->notOlderThan(3)->sizeIsGreater()->resultIsTrue();
    }

    /**
     * @inheritdoc
     */
    public function isEnabled()
    {
        /**
         * If socat is installed locally 
         * 
         * Then check if log file exits
         * Else then ping remote socat listener
         */
        if (`which netcat`) {
            $log_file = sprintf("%s/%s", $this->getBackupDirPath(), 'cmd.log');
            if (File::setFilePath($log_file)->fileExists()->notOlderThan(3)->sizeIsGreater()->resultIsTrue()) {
                return true;
            }
            $this->ping("socat_cmd_exist");
            return File::setFilePath($log_file)->fileExists()->notOlderThan(3)->sizeIsGreater()->resultIsTrue();
        } else {
            Helper::logger("Command Ran Failed: `which netcat`");
            return false;
        }
    }

    /**
     * Ping a container
     */
    protected function ping($cmd_verb)
    {
        $command = new Command([
            'command' => 'echo "$cmd_verb" | netcat $container_name 7777',
            'procEnv' => [
                'cmd_verb' => $cmd_verb,
                'container_name' => $this->container_name
            ]
        ]);

        if ($command->execute()) {
            $command->getOutput();
            Helper::logger("Command Ran Successfully: " . $command->getExecCommand());
            return File::setFilePath($this->getBackupFilePath())->fileExists()->notOlderThan(3)->sizeIsGreater()->resultIsTrue();
        } else {
            Helper::logger("Command Ran Failed: " . $command->getExecCommand() . ' Err: ' . $command->getError() . ' exit code:' . $exitCode = $command->getExitCode());
            return false;
        }
    }

    /**
     * Override
     * The socat script ran on cach_db has already zipped file to backup.gz
     */
    public function getBackupFilePathZipped()
    {
        return sprintf("%s/backup.gz", $this->getBackupDirPath()); // File name match getmsg.sh
    }
}
