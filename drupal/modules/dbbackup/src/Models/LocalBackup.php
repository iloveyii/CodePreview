<?php

namespace Drupal\dbbackup\Models;

use Drupal\dbbackup\Models\IBackup;
use Drupal\dbbackup\Models\Helper;
use Drupal\dbbackup\Models\File;
use Drupal\dbbackup\Models\BaseBackup;
use mikehaertl\shellcommand\Command;


class LocalBackup extends BaseBackup implements IBackup
{
    private $host, $db_name, $db_user, $db_pass;

    /**
     * Constructor
     * Backup for the mysql locally installed
     */
    public function __construct($host, $db_name, $db_user, $db_pass)
    {
        $this->host = $host;
        $this->db_name = $db_name;
        $this->db_user = $db_user;
        $this->db_pass = $db_pass;
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
        $command = new Command([
            'command' => '/usr/bin/mysqldump -h$host --no-tablespaces -u$user -p$pass $db > $path',
            'procEnv' => [
                'host' => $this->host,
                'user' => $this->db_user,
                'pass' => $this->db_pass,
                'db' => $this->db_name,
                'path' => $this->getBackupFilePath()
            ]
        ]);

        if ($command->execute()) {
            $command->getOutput();
            Helper::logger("Command Ran Successfully: " . $command->getExecCommand());
            sleep(3);
            return File::setFilePath($this->getBackupFilePath())->fileExists()->notOlderThan(3)->sizeIsGreater()->resultIsTrue();
        } else {
            Helper::logger("Command Ran Failed: " . $command->getExecCommand() . ' Err: ' . $command->getError() . ' exit code:' .  $command->getExitCode());
            return false;
        }
    }

    /**
     * @inheritdoc
     */
    public function isEnabled()
    {
        if (File::setFilePath($this->getBackupFilePath())->fileExists()->notOlderThan(3)->sizeIsGreater()->resultIsTrue()) {
            Helper::logger("isEnabled file is new");
            //return true;
        }
        Helper::logger("isEnabled running");
        if (`which mysqldump`) {
            Helper::logger("which mysqldump - ran Successfully: ");
        }
        return true;
    }
}
