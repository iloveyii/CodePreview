<?php

namespace Drupal\menu\Plugin\Block;

use Drupal\Core\Block\BlockBase;
use Symfony\Component\DependencyInjection\ContainerInterface;
use Drupal\simplify_menu\MenuItems;
use Drupal\Core\Plugin\ContainerFactoryPluginInterface;
use Drupal\Core\Session\AccountInterface;
use Drupal\application\Util\Helper;

/**
 * Provides a 'Menu' Block.
 *
 * @Block(
 *   id = "bs5menu_block",
 *   admin_label = @Translation("BS 5 Menu Block"),
 *   category = @Translation("Custom"),
 * )
 */
class MenuBlock extends BlockBase implements ContainerFactoryPluginInterface
{
  /**
   * MenuItems definition.
   *
   * @var \Drupal\simplify_menu\MenuItems
   */
  protected $menuItems;

  /**
   * MenuItemsTwigExtension constructor.
   *
   * @param \Drupal\simplify_menu\MenuItems $menuItems
   *   The MenuItems service.
   */

  /**
   * @var AccountInterface $account
   */
  protected $account;

  /**
   * @param array $configuration
   * @param string $plugin_id
   * @param mixed $plugin_definition
   * @param \Drupal\Core\Session\AccountInterface $account
   */
  public function __construct(array $configuration, $plugin_id, $plugin_definition, AccountInterface $account, MenuItems $menuItems)
  {
    parent::__construct($configuration, $plugin_id, $plugin_definition);
    $this->account = $account;
    $this->menuItems = $menuItems;
  }

  /**
   * @param \Symfony\Component\DependencyInjection\ContainerInterface $container
   * @param array $configuration
   * @param string $plugin_id
   * @param mixed $plugin_definition
   *
   * @return static
   */
  public static function create(ContainerInterface $container, array $configuration, $plugin_id, $plugin_definition)
  {
    return new static(
      $configuration,
      $plugin_id,
      $plugin_definition,
      $container->get('current_user'),
      new MenuItems($container->get('menu.link_tree'))
    );
  }


  /**
   * {@inheritdoc}
   */
  public function build()
  {
    $items = $this->menuItems->getMenuTree('main');

    $menu_tree = $items['menu_tree'];
    $challenges_id = array_search('CHALLENGES', array_column($menu_tree, 'text'));
    // print_r($menu_tree[$challenges_id]);

    $menu = [
      'text' => 'Challenge #214 go',
      'url' => '/challenges/predict-hits-wdr-domain-lrrk2',
    ];

    $sub_menu = [
      [
        'text' => 'Announcement',
        'url' => '/challenges/predict-hits-wdr-domain-lrrk2',
      ],
      [
        'text' => 'Computation methods',
        'url' => '/challenge-1/computational-methods',
      ]
    ];

    if ($challenges_id !== false) {
      // $menu_tree[$challenges_id]['submenu'][] = $this->addMenu($menu, $sub_menu);
      $m1 = $this->textKeyed($menu_tree[$challenges_id]['submenu']);
      $m2 = $this->textKeyed($this->buildChallengesMenu());
      $merged = array_merge($m2, $m1); // m1 take precedence
      ksort($merged, SORT_STRING);
      $menu_tree[$challenges_id]['submenu'] =  $merged;
      $items['menu_tree'] = $menu_tree;
    }

    return [
      '#theme' => 'main',
      '#items' => $items,
      '#attached' => [
        'library' => [
          'menu/menu'
        ]
      ]
    ];
  }

  private function textKeyed($menus)
  {
    $menus_text_keyed = [];
    foreach ($menus as $menu) {
      if (isset($menu['text'])) {
        if (isset($menu['submenu'])) {
          $menu['submenu'] = $this->textKeyed($menu['submenu']);
        }
        $menus_text_keyed[$menu['text']] = $menu;
      } else {
        $menus_text_keyed[] = $menu;
      }
    }

    return $menus_text_keyed;
  }

  /**
   *
   */
  private function addMenu($menu, $sub_menu)
  {
    $menu['active_trail'] = '';
    $menu['active'] = '';

    // Active & active trail
    foreach ($sub_menu as &$smenu) {
      $smenu['active_trail'] = '';
      $smenu['active'] = '';
    }
    $menu['submenu'] = $sub_menu;

    return $menu;
  }

  /**
   * Get all challenges
   */
  private function buildChallengesMenu()
  {
    $state_service = \Drupal::service('state.service');
    $competition_service = \Drupal::service('competition.service');
    $challenges = $competition_service->getAll();

    $menus = [];

    foreach ($challenges as $challenge) {
      $menu = [];
      $submenus = [];
      $challenge = $state_service->getLatestVersion($challenge);
      $current_state = $state_service->getStateId($challenge);

      // Announcement
      if ($state_service->stateIsBetween('announcement', 'show_ho_files', $current_state)) {
        $menu = [
          'text' => $challenge->getTitle(),
          'url' => sprintf("/challenges/%s", Helper::urlify($challenge->get('field_challenge_title')->getString())),
        ];
        $submenus[] = [
          'text' => 'Announcement',
          'url' => sprintf("/challenges/%s", Helper::urlify($challenge->get('field_challenge_title')->getString())),
        ];
      }

      // Computational methods - show menu criteria
      $field_show_in_hi_comp_meth = $challenge->field_show_in_hi_comp_meth->value;
      $field_show_in_ho_comp_meth = $challenge->field_show_in_ho_comp_meth->value;
      $field_show_in_ms_comp_meth = $challenge->field_show_in_ms_comp_meth->value;

      // if ($state_service->stateIsBetween('panel_review_closes', 'show_ho_files', $current_state)) {
      if ($field_show_in_hi_comp_meth == 1 || $field_show_in_ho_comp_meth == 1 || $field_show_in_ms_comp_meth == 1) {
        $submenus[] = [
          'text' => 'Computation methods',
          'url' => sprintf("/challenges/%s/computational-methods", Helper::urlify($challenge->get('field_challenge_title')->getString())),
        ];
      }

      // Other conneccted pages to this challenge by taxonomy vocabulary: Challenges
      $term_id = Helper::getTermIdByName('challenges', $challenge->getTitle());
      $nodes = \Drupal::entityTypeManager()->getStorage('node')->loadByProperties(['type' => 'general_page', 'field_challenges' => [$term_id]]);

      foreach ($nodes as $node) {
        $node = $state_service->getLatestVersion($node);
        if (!$node->isPublished()) {
          continue;
        }
        $label = $node->label();
        if ($node->hasField('field_menu_label')) {
          $label = $node->get('field_menu_label')->getString();
        }
        $submenus[] = [
          'text' => empty($label) ? 'Link' : $label,
          'url' => \Drupal::service('path_alias.manager')->getAliasByPath('/node/' . $node->id()),
        ];
      }

      $menus[] = $this->addMenu($menu, $submenus);
    }
    return $menus;
  }

  /**
   * Don't cache this page
   */
  public function getCacheMaxAge()
  {
    return 0;
  }
}
