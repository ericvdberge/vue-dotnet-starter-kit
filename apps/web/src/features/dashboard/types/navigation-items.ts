export interface NavigationGroup {
    title: string;
    items: NavigationItem[];
}

export interface NavigationItem {
    title: string;
    icon: any; // Replace with the appropriate type for your icons
    to: string;
}