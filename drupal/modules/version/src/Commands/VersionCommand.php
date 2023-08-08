<?php

namespace Drupal\version\Commands;

use Drush\Commands\DrushCommands;
use Drupal\Core\Site\Settings;
use Exception;
use Drupal\version\Service\VersionService;
use Symfony\Component\DependencyInjection\ContainerInterface;
use Drupal\Core\Entity\EntityTypeManager;


/**
 * A drush command file
 * 
 * @package Drupal\version\Commands
 */
class VersionCommand extends DrushCommands
{
    protected $versionService;

    public function __construct(EntityTypeManager $em, $versionService = null)
    {
        $this->versionService = $versionService;
    }

    /**
     * {@inheritdoc}
     */
    public static function create(EntityTypeManager $em, ContainerInterface $container)
    {
        return new static(
            $em,
            $container->get('versions.service')
        );
    }

    /**
     * Drush command that displays the current version.
     * @command version:current
     * @aliases version:c v:c
     * @usage version:current
     */
    public function current($options = [])
    {
        $this->get();
    }

    /**
     * Drush command that displays the given text.
     *
     * @command version:get
     * @aliases version:g v:g
     * @option uppercase
     *   Uppercase the message.
     * @usage version:get --uppercase 
     */
    public function get($options = ['uppercase' => FALSE])
    {
        $version =  \Drupal::service('versions.service')->currentEnv();
        if ($options['uppercase']) {
            $version = strtoupper($version);
        }
        $this->output()->writeln($version);
    }

    private function _set($version)
    {
        // 1 - set in .env
        \Drupal::service('versions.service')->replaceInEnv($version);
        // 2 - set in build/install.txt
        \Drupal::service('versions.service')->replaceInInstall($version);
        // 3 - set in database table version
        \Drupal::service('versions.service')->replaceInDb($version);
    }

    /**
     * Drush command that displays the given text.
     * Format v 9.1.2.3
     *
     * @command version:bump
     * @aliases version:b v:b
     * @option major
     *   Major revision.
     * @option minor
     *   Minor revision.
     * @option bug
     *   Bug fix release.
     * @option build
     *   Build number.
     * @option dry
     *   Dry run.
     * @usage version:bump --major --minor --bug --build --dry
     */
    public function bump($options = ['major' => FALSE, 'minor' => FALSE, 'bug' => FALSE, 'build' => FALSE, 'dry' => FALSE])
    {
        $version = strtoupper(\Drupal::service('versions.service')->currentEnv());
        $numeric = trim(str_replace('V', '', $version));
        $parts = explode('.', $numeric);

        // Check major
        if ($options['major']) {
            $parts[0] = $parts[0] + 1;
        }
        // Check minor
        if ($options['minor']) {
            $parts[1] = $parts[1] + 1;
        }
        // Check bug
        if ($options['bug']) {
            $parts[2] = $parts[2] + 1;
        }
        // Check build
        if ($options['build']) {
            $parts[3] = $parts[3] + 1;
        }
        // Make v
        $version = sprintf("v%d.%d.%d.%d", $parts[0], $parts[1], $parts[2], $parts[3]);

        if (!$options['dry']) {
            $this->_set($version);
        }
        $this->output()->writeln(sprintf("Bumped version: %s", $version));
    }
}
