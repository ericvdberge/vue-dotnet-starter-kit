import { Home, Folder } from "lucide-vue-next"

export const useNavigationItems = () => {
    return [
        {
            title: 'Dashboard',
            icon: Home,
            to: '/dashboard'
        },
        {
            title: 'Projects',
            icon: Folder, 
            to: '/dashboard/projects'
        }
    ]
}