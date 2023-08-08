<?php

namespace Drupal\actioner\Event;


use Drupal\Component\EventDispatcher\Event;
use Drupal\user\UserInterface;

/**
 * Event that is fired when a user logs in.
 */
class UserLoginEvent extends Event
{

    const EVENT_NAME = 'actioner_user_login';

    /**
     * The user account.
     *
     * @var \Drupal\user\UserInterface
     */
    public $account;

    /**
     * Constructs the object.
     *
     * @param \Drupal\user\UserInterface $account
     *   The account of the user logged in.
     */
    public function __construct(UserInterface $account)
    {
        $this->account = $account;
    }
}
