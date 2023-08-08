<?php

namespace Drupal\dbbackup\Models;

use mikehaertl\shellcommand\Command;


class Helper
{
    public static function fileExists($file_path) // hours
    {
        if (file_exists($file_path)) {
            return true;
        }
        Helper::logger("File not exists: " . $file_path);
        return false;
    }

    public static function logger($msg)
    {
        // echo $msg . PHP_EOL;
        \Drupal::logger('db_backup')->notice($msg);
    }

    public static function getBackupFilePathZipped($backup_file_path)
    {
        $cmd = sprintf("chmod 777 %s/gzip.sh", __DIR__);
        sleep(1);
        $cmd = sprintf("%s/gzip.sh %s", __DIR__, $backup_file_path);
        Helper::logger("CMD Ran: " . $cmd);
        shell_exec($cmd);
        sleep(5);
        return sprintf("%s.gz", $backup_file_path);
    }

    public static function node_type_get_names()
    {
        return array_map(function ($bundle_info) {
            return $bundle_info['label'];
        }, \Drupal::service('entity_type.bundle.info')
            ->getBundleInfo('node'));
    }

    public static function getLatestFile($path, $mask = '/.*\.gz/')
    {
        $files = \Drupal::service('file_system')->scanDirectory($path, $mask);
        usort($files, function ($a, $b) {
            return filemtime(\Drupal::service('file_system')->realpath($b->uri)) - filemtime(\Drupal::service('file_system')->realpath($a->uri));
        });
        if (is_array($files) && count($files) > 0) {
            return \Drupal::service('file_system')->realpath($files[0]->uri);
        }
        return self::getLatestFileCustom($path, '*.gz');
    }

    public static function getLatestFileCustom($path, $mask = '*.gz')
    {
        $path = $files = \Drupal::service('file_system')->realpath($path);
        $dir = sprintf("%s/%s", $path, $mask);
        $files = glob($dir);
        usort($files, function ($a, $b) {
            return filemtime($b) - filemtime($a);
        });
        if (is_array($files) && count($files) > 0) {
            return $files[0];
        }

        return 'No_path_found for path: ' . $path . ', mask: ' . $mask;
    }

    /**
     * Unzip (*.zip) file to a destination directory
     * 
     * @param string $file_path
     *  Full path of zip file
     * @param string $destination
     *  Full path of directory to unzip to
     */
    public static function unZip($file_path, $destination)
    {
        $command = new Command([
            'command' => 'unzip -o $path -d $destination',
            'procEnv' => [
                'path' => $file_path,
                'destination' => $destination
            ]
        ]);

        Helper::logger("Command for unzip ran -  file: {$file_path}, des : {$destination} " . $command->getExecCommand());

        if (Helper::fileExists($file_path) &&  $command->execute()) {
            return $command->getOutput();
        } else {
            Helper::logger(sprintf("The unzip command failed with error: %s & exit code: %s", $command->getError(), $command->getExitCode()));
            return false;
        }
    }

    /**
     * Get file name without extension
     * @param string $file_path
     *  Full path 
     */
    public static function getFileName($file_path)
    {
        $info = pathinfo($file_path);
        return $info['filename'];
    }

    /**
     * Run shell commands
     * 
     * @param string $cmd 
     *  Command to run
     * @param array $arguments
     *  array of arguments to command
     */
    public static function runCommand($cmd, $arguments = [])
    {
        $command = new Command([
            'command' => $cmd,
            'procEnv' => $arguments
        ]);

        if ($command->execute()) {
            Helper::logger(sprintf("Command `%s` ran Successfully with arguments: %s, and output: %s ", $command->getExecCommand(), implode(', ', $arguments), $command->getOutput()));
            return true;
        } else {
            Helper::logger("Command failed: " . $command->getExecCommand() . ' Err: ' . $command->getError() . ' exit code:' . $command->getExitCode());
            return false;
        }
    }

    /**
     * Clear db log
     */
    public static function clearDblog()
    {
        $query = \Drupal::database()->delete('watchdog');
        return $query->execute();
    }

    /**
     * A multi-dimensional array in terms of ul li
     * 
     * @param array $info 
     */
    public function showInfo($info)
    {
        $ul = '<ul>';
        foreach ($info as $key => $value) {
            if (is_array($value)) {
                $ul .= $this->showInfo($value);
            } else {
                $ul .= sprintf("%s -> %s", $key, $value);
            }
        }
        $ul .= '</ul>';

        return $ul;
    }
}
